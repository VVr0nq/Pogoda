using System;
using System.Collections.Generic;
using System.IO;


namespace Pogoda
{
    class Program
    {
        static String[][] readFile()
        {
            List<String[]> list = new List<String[]>();
            using (StreamReader reader = new StreamReader("pogoda.txt"))
            {
                while (!reader.EndOfStream)
                {
                    String line = reader.ReadLine();
                    String[] values = line.Split(';');
                    list.Add(values);
                }
            }
            return list.ToArray();
        }

        static void ShowDays()
        {
            String[][] dane = readFile();

            int dni_ponizej_15 = 0;
            int dni_powyzej_15_min = 0;
            int dni_powyzej_15_max = 0;

            double zbiornik = 25000;
            double zbiornik2 = 0;
            double zbiornik3 = 0;
            int count = 0;
            List<int> dolanie = new List<int>();

            foreach (String[] row in dane)
            {
                String rain_coma = row[1].Replace('.', ',');
                int temp = Int32.Parse(row[0]);
                Double rain = Double.Parse(rain_coma);
                zbiornik += Math.Ceiling((700 * rain));

                count += 1;

                Double paruje = Math.Ceiling((0.0003 * Math.Pow(temp, 1.5)) * zbiornik);


                if (temp < 15)
                {
                    dni_ponizej_15++;

                    if (rain == 0)
                    {
                        zbiornik -= paruje;

                    }
                    if (zbiornik <= 0)
                    {
                        zbiornik = 0;
                        zbiornik += 25000;
                        zbiornik2 += 25000;
                    }

                    if (zbiornik > 25000)
                    {
                        zbiornik = 25000;
                    }

                }



                if (temp > 15 && rain < 0.6)
                {

                    if (rain == 0)
                    {
                        zbiornik -= paruje;
                    }

                    if (temp < 30)
                    {

                        if (zbiornik < 12000)
                        {
                            zbiornik3 = (25000 - zbiornik);
                            zbiornik += zbiornik3;
                            zbiornik += zbiornik3;
                            dolanie.Add(count - 1);

                        }

                        if (zbiornik > 25000)
                        {
                            zbiornik = 25000;
                        }

                        zbiornik -= 12000;
                    }

                    if (temp > 30 && rain < 0.6)
                    {
                        dni_powyzej_15_min++;

                        if (rain == 0)
                        {
                            zbiornik -= paruje;
                        }

                        if (zbiornik < 24000)
                        {
                            zbiornik3 = (25000 - zbiornik);
                            zbiornik += zbiornik3;
                            zbiornik2 += zbiornik3;
                            dolanie.Add(count - 1);

                        }

                        zbiornik -= 24000;
                    }

                }


                if (temp > 15 && rain > 0.6)
                {
                    dni_powyzej_15_max++;

                    if (zbiornik > 25000)
                    {
                        zbiornik = 25000;
                    }
                }
                Console.WriteLine(count + " Poziom zbiornika " + zbiornik + "    dolano w sumie " + zbiornik2);
            }




            Console.WriteLine("Ilosc dni : " + dane.Length);
            Console.WriteLine("ilosc dni z temp ponizej 15°C: " + dni_ponizej_15);
            Console.WriteLine("ilosc dni z temp ponizej 15°C i opadami ponizej 0,6 l/m2: " + dni_powyzej_15_min);
            Console.WriteLine("ilosc dni z temp ponizej 15°C i opadami powyzej 0,6 l/m2: " + dni_powyzej_15_max);
            Console.WriteLine(" ");
            Console.WriteLine("Pierwszy dzien w ktorym dolano wode " + dolanie[0]);
            Console.WriteLine("W sumie dolano" + zbiornik2);


        }


        static void Main(string[] args)
        {

            readFile();
            ShowDays();
            Console.ReadLine();

        }






    }
}