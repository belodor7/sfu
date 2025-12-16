namespace ConsoleApp6_Mag
{
    using System;
    using System.Collections.Generic;

    public static class AutoTest
    {
        public static int Run()
        {
            var factories = new List<Func<Mag>>{
                () => new Fire_Mag(),
                () => new Wood_Mag(),
                () => new Water_Mag(),
                () => new Dirt_Mag(),
                () => new Metal_Mag(),
                () => new Normal_Mag()
            };
            var names = new List<string>{"Fire","Wood","Water","Dirt","Metal","Normal"};

            int total = 0, playerWins = 0, enemyWins = 0, others = 0;
            var examples = new List<string>();
            var detailedLogs = new List<List<string>>();
            var invariantViolations = new List<string>();
            for (int bi = 0; bi < factories.Count; bi++)
            {
                for (int bj = 0; bj < factories.Count; bj++)
                {
                    if (bi == bj) continue; // player cannot choose two identical
                    for (int ei = 0; ei < factories.Count; ei++)
                    {
                        for (int ej = 0; ej < factories.Count; ej++)
                        {
                            if (ei == ej) continue; // enemy mags must be different
                            total++;
                            var b1 = factories[bi]();
                            var b2 = factories[bj]();
                            var e1 = factories[ei]();
                            var e2 = factories[ej]();

                            var (res, maybeLog) = SimulateFightWithLog(e1, e2, b1, b2);
                            if (res == 1) playerWins++;
                            else if (res == -1) enemyWins++;
                            else others++;

                            if (res != 1 && examples.Count < 20)
                            {
                                examples.Add($"Player: {names[bi]},{names[bj]} vs Enemy: {names[ei]},{names[ej]} => result={res}");
                            }
                            if (res != 1 && detailedLogs.Count < 5)
                            {
                                detailedLogs.Add(maybeLog);
                            }

                            // validate invariants on the final state from maybeLog
                            ValidateInvariants(maybeLog, names[bi], names[bj], names[ei], names[ej], invariantViolations);
                        }
                    }
                }
            }

            Console.WriteLine("===== AUTOTEST RESULT =====");
            Console.WriteLine($"Total scenarios: {total}");
            Console.WriteLine($"Player wins: {playerWins}");
            Console.WriteLine($"Enemy wins: {enemyWins}");
            Console.WriteLine($"Other outcomes: {others}");
            Console.WriteLine();
            Console.WriteLine("Sample non-player-win scenarios:");
            foreach (var s in examples)
                Console.WriteLine(s);

            Console.WriteLine();
            Console.WriteLine("Detailed logs for first non-player-win scenarios:");
            int idx = 0;
            foreach (var log in detailedLogs)
            {
                Console.WriteLine($"--- Detailed log #{++idx} ---");
                foreach (var line in log)
                    Console.WriteLine(line);
                Console.WriteLine();
            }

            if (invariantViolations.Count > 0)
            {
                Console.WriteLine();
                Console.WriteLine("Invariant violations found:");
                foreach (var v in invariantViolations)
                    Console.WriteLine(v);
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("No invariant violations detected.");
            }

            Console.WriteLine("===== END AUTOTEST =====");
            return 0;
        }

        // playerSpec and enemySpec are comma-separated element names, e.g. "Normal,Dirt" "Normal,Water"
        public static void RunScenario(string playerSpec, string enemySpec)
        {
            string[] p = playerSpec.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            string[] e = enemySpec.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            if (p.Length != 2 || e.Length != 2)
            {
                Console.WriteLine("Scenario format: <player1,player2> <enemy1,enemy2>");
                return;
            }

            Mag b1 = CreateByName(p[0]);
            Mag b2 = CreateByName(p[1]);
            Mag e1 = CreateByName(e[0]);
            Mag e2 = CreateByName(e[1]);

            var (res, log) = SimulateFightWithLog(e1, e2, b1, b2);
            Console.WriteLine($"Scenario: Player {p[0]},{p[1]} vs Enemy {e[0]},{e[1]} => result={res}");
            foreach (var line in log) Console.WriteLine(line);
        }

        static Mag CreateByName(string name)
        {
            return name.ToLower() switch
            {
                "fire" => new Fire_Mag(),
                "wood" => new Wood_Mag(),
                "water" => new Water_Mag(),
                "dirt" => new Dirt_Mag(),
                "metal" => new Metal_Mag(),
                _ => new Normal_Mag(),
            };
        }

        // Run N randomized fights with fixed seed and export CSV
        public static void RunRandomized(int count, int seed, string outPath = "autotest_random.csv")
        {
            var rnd = new Random(seed);
            var elements = new[] { "Fire", "Wood", "Water", "Dirt", "Metal", "Normal" };
            using var sw = new System.IO.StreamWriter(System.IO.Path.Combine("ConsoleApp6_Mag", outPath));
            sw.WriteLine("player1,player2,enemy1,enemy2,result");
            int playerWins = 0, enemyWins = 0, draws = 0;
            for (int i = 0; i < count; i++)
            {
                // pick two distinct player elements
                int p1 = rnd.Next(0, elements.Length);
                int p2;
                do { p2 = rnd.Next(0, elements.Length); } while (p2 == p1);
                int e1 = rnd.Next(0, elements.Length);
                int e2;
                do { e2 = rnd.Next(0, elements.Length); } while (e2 == e1);

                var b1 = CreateByName(elements[p1]);
                var b2 = CreateByName(elements[p2]);
                var en1 = CreateByName(elements[e1]);
                var en2 = CreateByName(elements[e2]);

                var (res, _) = SimulateFightWithLog(en1, en2, b1, b2);
                if (res == 1) playerWins++; else if (res == -1) enemyWins++; else draws++;

                sw.WriteLine($"{elements[p1]},{elements[p2]},{elements[e1]},{elements[e2]},{res}");
            }
            sw.Flush();
            Console.WriteLine("Randomized run finished.");
            Console.WriteLine($"Total: {count}, playerWins={playerWins}, enemyWins={enemyWins}, draws={draws}");
            Console.WriteLine($"CSV saved to: {System.IO.Path.Combine("ConsoleApp6_Mag", outPath)}");
        }

        // Returns: 1 => player wins, -1 => enemy wins, 0 => other/draw
        static int SimulateFight(Mag e1, Mag e2, Mag b1, Mag b2)
        {
            // replicate SpecAttack sequence from Program.SpecAttack
            // b1 special vs e1
            if (b1.KillsElement == e1.Element)
                e1.IsDeath = true;
            else
            {
                if (e1.KillsElement == b1.Element)
                    b1.IsDeath = true;
            }

            // e2 special vs b2 (program had e2 then b2 sequence)
            if (e2.KillsElement == b2.Element)
                b2.IsDeath = true;
            else
            {
                if (b2.KillsElement == e2.Element)
                    e2.IsDeath = true;
            }

            // Check immediate victory
            if (e1.IsDeath && e2.IsDeath) return 1;
            if (b1.IsDeath && b2.IsDeath) return -1;

            // Attack loop: replicate Program.Attack order
            while ((!b1.IsDeath || !b2.IsDeath) && (!e1.IsDeath || !e2.IsDeath))
            {
                if (!b1.IsDeath)
                {
                    if (!e1.IsDeath)
                    {
                        e1.Health -= b1.Damage;
                        if (e1.Health <= 0) e1.IsDeath = true;
                    }
                    else if (!e2.IsDeath)
                    {
                        e2.Health -= b1.Damage;
                        if (e2.Health <= 0) e2.IsDeath = true;
                    }
                }

                if (!b2.IsDeath)
                {
                    if (!e2.IsDeath)
                    {
                        e2.Health -= b2.Damage;
                        if (e2.Health <= 0) e2.IsDeath = true;
                    }
                    else if (!e1.IsDeath)
                    {
                        e1.Health -= b2.Damage;
                        if (e1.Health <= 0) e1.IsDeath = true;
                    }
                }

                if (!e1.IsDeath)
                {
                    if (!b1.IsDeath)
                    {
                        b1.Health -= e1.Damage;
                        if (b1.Health <= 0) b1.IsDeath = true;
                    }
                    else if (!b2.IsDeath)
                    {
                        b2.Health -= e1.Damage;
                        if (b2.Health <= 0) b2.IsDeath = true;
                    }
                }

                if (!e2.IsDeath)
                {
                    if (!b2.IsDeath)
                    {
                        b2.Health -= e2.Damage;
                        if (b2.Health <= 0) b2.IsDeath = true;
                    }
                    else if (!b1.IsDeath)
                    {
                        b1.Health -= e2.Damage;
                        if (b1.Health <= 0) b1.IsDeath = true;
                    }
                }

                if (e1.IsDeath && e2.IsDeath) return 1;
                if (b1.IsDeath && b2.IsDeath) return -1;
            }

            if (e1.IsDeath && e2.IsDeath) return 1;
            if (b1.IsDeath && b2.IsDeath) return -1;
            return 0;
        }

        // Returns tuple: (result, log)
        static (int, List<string>) SimulateFightWithLog(Mag e1src, Mag e2src, Mag b1src, Mag b2src)
        {
            // clone objects to avoid mutating originals
            Mag e1 = CloneMag(e1src);
            Mag e2 = CloneMag(e2src);
            Mag b1 = CloneMag(b1src);
            Mag b2 = CloneMag(b2src);

            var log = new List<string>();

            log.Add($"Start: Player [{b1.Element},{b2.Element}] HP [{b1.Health},{b2.Health}] vs Enemy [{e1.Element},{e2.Element}] HP [{e1.Health},{e2.Health}]");

            // spec attacks
            log.Add("Applying special abilities (SpecAttack order)");
            if (b1.KillsElement == e1.Element)
            {
                e1.IsDeath = true; log.Add($"b1 special killed e1");
            }
            else
            {
                if (e1.KillsElement == b1.Element) { b1.IsDeath = true; log.Add($"e1 special killed b1"); }
                else log.Add($"b1 special no kill");
            }

            if (e2.KillsElement == b2.Element) { b2.IsDeath = true; log.Add($"e2 special killed b2"); }
            else { if (b2.KillsElement == e2.Element) { e2.IsDeath = true; log.Add($"b2 special killed e2"); } else log.Add($"e2 special no kill"); }

            log.Add($"After specials HP: Player [{b1.Health},{b2.Health}] Dead [{b1.IsDeath},{b2.IsDeath}] vs Enemy [{e1.Health},{e2.Health}] Dead [{e1.IsDeath},{e2.IsDeath}]");

            if (e1.IsDeath && e2.IsDeath) { log.Add("Enemy both dead => player wins"); return (1, log); }
            if (b1.IsDeath && b2.IsDeath) { log.Add("Player both dead => enemy wins"); return (-1, log); }

            int round = 0;
            while ((!b1.IsDeath || !b2.IsDeath) && (!e1.IsDeath || !e2.IsDeath))
            {
                round++;
                log.Add($"-- Round {round} start --");
                if (!b1.IsDeath)
                {
                    if (!e1.IsDeath)
                    {
                        e1.Health -= b1.Damage;
                        log.Add($"b1 hits e1 for {b1.Damage} => e1 HP={e1.Health}");
                        if (e1.Health <= 0) { e1.IsDeath = true; log.Add("e1 died"); }
                    }
                    else if (!e2.IsDeath)
                    {
                        e2.Health -= b1.Damage;
                        log.Add($"b1 hits e2 for {b1.Damage} => e2 HP={e2.Health}");
                        if (e2.Health <= 0) { e2.IsDeath = true; log.Add("e2 died"); }
                    }
                }

                if (!b2.IsDeath)
                {
                    if (!e2.IsDeath)
                    {
                        e2.Health -= b2.Damage;
                        log.Add($"b2 hits e2 for {b2.Damage} => e2 HP={e2.Health}");
                        if (e2.Health <= 0) { e2.IsDeath = true; log.Add("e2 died"); }
                    }
                    else if (!e1.IsDeath)
                    {
                        e1.Health -= b2.Damage;
                        log.Add($"b2 hits e1 for {b2.Damage} => e1 HP={e1.Health}");
                        if (e1.Health <= 0) { e1.IsDeath = true; log.Add("e1 died"); }
                    }
                }

                if (!e1.IsDeath)
                {
                    if (!b1.IsDeath)
                    {
                        b1.Health -= e1.Damage;
                        log.Add($"e1 hits b1 for {e1.Damage} => b1 HP={b1.Health}");
                        if (b1.Health <= 0) { b1.IsDeath = true; log.Add("b1 died"); }
                    }
                    else if (!b2.IsDeath)
                    {
                        b2.Health -= e1.Damage;
                        log.Add($"e1 hits b2 for {e1.Damage} => b2 HP={b2.Health}");
                        if (b2.Health <= 0) { b2.IsDeath = true; log.Add("b2 died"); }
                    }
                }

                if (!e2.IsDeath)
                {
                    if (!b2.IsDeath)
                    {
                        b2.Health -= e2.Damage;
                        log.Add($"e2 hits b2 for {e2.Damage} => b2 HP={b2.Health}");
                        if (b2.Health <= 0) { b2.IsDeath = true; log.Add("b2 died"); }
                    }
                    else if (!b1.IsDeath)
                    {
                        b1.Health -= e2.Damage;
                        log.Add($"e2 hits b1 for {e2.Damage} => b1 HP={b1.Health}");
                        if (b1.Health <= 0) { b1.IsDeath = true; log.Add("b1 died"); }
                    }
                }

                log.Add($"After round {round}: Player HP [{b1.Health},{b2.Health}] Dead [{b1.IsDeath},{b2.IsDeath}] vs Enemy HP [{e1.Health},{e2.Health}] Dead [{e1.IsDeath},{e2.IsDeath}]");

                if (e1.IsDeath && e2.IsDeath) { log.Add("Enemy both dead => player wins"); return (1, log); }
                if (b1.IsDeath && b2.IsDeath) { log.Add("Player both dead => enemy wins"); return (-1, log); }
                if (round > 1000) { log.Add("Round limit exceeded => draw"); return (0, log); }
            }

            if (e1.IsDeath && e2.IsDeath) { log.Add("Enemy both dead => player wins"); return (1, log); }
            if (b1.IsDeath && b2.IsDeath) { log.Add("Player both dead => enemy wins"); return (-1, log); }
            log.Add("No decisive outcome => draw");
            return (0, log);
        }

        static Mag CloneMag(Mag src)
        {
            // shallow clone by creating same concrete type
            return src.Element switch
            {
                Element.Fire => new Fire_Mag(){ Damage = src.Damage, Health = src.Health, IsDeath = src.IsDeath },
                Element.Wood => new Wood_Mag(){ Damage = src.Damage, Health = src.Health, IsDeath = src.IsDeath },
                Element.Water => new Water_Mag(){ Damage = src.Damage, Health = src.Health, IsDeath = src.IsDeath },
                Element.Dirt => new Dirt_Mag(){ Damage = src.Damage, Health = src.Health, IsDeath = src.IsDeath },
                Element.Metal => new Metal_Mag(){ Damage = src.Damage, Health = src.Health, IsDeath = src.IsDeath },
                _ => new Normal_Mag(){ Damage = src.Damage, Health = src.Health, IsDeath = src.IsDeath },
            };
        }

        static void ValidateInvariants(List<string> log, string b1name, string b2name, string e1name, string e2name, List<string> outViolations)
        {
            // The log's last lines contain final state like: "After round N: Player HP [x,y] Dead [a,b] vs Enemy HP [u,v] Dead [c,d]"
            for (int i = log.Count - 1; i >= 0; i--)
            {
                var line = log[i];
                if (line.StartsWith("After round") || line.StartsWith("After specials HP"))
                {
                    // try to parse numbers
                    try
                    {
                        var parts = line.Split(']');
                        // naive parse: find all integers in line
                        var nums = new List<int>();
                        foreach (System.Text.RegularExpressions.Match m in System.Text.RegularExpressions.Regex.Matches(line, "-?\\d+"))
                        {
                            var s = m.Value ?? "0";
                            if (int.TryParse(s, out int v)) nums.Add(v);
                        }
                        // expectation: order -> Player HP x,y ; Dead a,b ; Enemy HP u,v ; Dead c,d
                        if (nums.Count >= 8)
                        {
                            int p1hp = nums[0], p2hp = nums[1];
                            int p1dead = nums[2], p2dead = nums[3];
                            int e1hp = nums[4], e2hp = nums[5];
                            int e1dead = nums[6], e2dead = nums[7];

                            // invariant 1: if dead flag true then HP <= 0 OR it was killed by special (special kills don't reduce HP). We cannot detect special here, so at least check HP<=0 when dead.
                            if (p1dead == 1 && p1hp > 0)
                                outViolations.Add($"Invariant: player1 dead flag set but HP>0 ({b1name}) - line: {line}");
                            if (p2dead == 1 && p2hp > 0)
                                outViolations.Add($"Invariant: player2 dead flag set but HP>0 ({b2name}) - line: {line}");
                            if (e1dead == 1 && e1hp > 0)
                                outViolations.Add($"Invariant: enemy1 dead flag set but HP>0 ({e1name}) - line: {line}");
                            if (e2dead == 1 && e2hp > 0)
                                outViolations.Add($"Invariant: enemy2 dead flag set but HP>0 ({e2name}) - line: {line}");
                        }
                    }
                    catch { }
                    break;
                }
            }
        }
    }
}
