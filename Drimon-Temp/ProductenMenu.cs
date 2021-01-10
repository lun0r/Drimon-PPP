﻿using System;
using System.Collections.Generic;

namespace Drimon_Temp
{
    internal class ProductMenu
    {
        public static void MenuProductHoofdmenu()
        {
            Console.WriteLine($"1.Product zoeken \n2.Product Toevoegen \n3.Schotels \n4.Terug naar hoofdmenu");
            switch (Menu.MethodeKiezer(3))
            {
                case 1:
                    Console.Clear();
                    OverzichtProductLijst();
                    MenuProductZoeken();
                    break;

                case 2:
                    Console.Clear();
                    MethodeProductToevoegen();
                    break;

                case 3:
                    Console.Clear();
                    SchotelMenu.MenuSchotelHoofdmenu();
                    break;

                case 4:
                    Menu.MenuHoofdmenu();
                    break;
            }
        }

        public static void MenuProductZoeken()
        {
            Console.WriteLine($"\n1.Selecteer product\n2.Terug\n\nZoek op: \n  3.Naam\n ");
            string userinput;
            switch (Menu.MethodeKiezer(3))
            {
                case 1:
                    Console.Clear();
                    OverzichtProductLijst();
                    Console.WriteLine("\nGeef het ID gevolgd door 'enter':");
                    int input = Menu.MethodeCheckforInt(Console.ReadLine());
                    Console.Clear();
                    MenuProductEnkel(input, "zoeken");
                    break;

                case 2:
                    Console.Clear();
                    MenuProductHoofdmenu();
                    break;

                case 3:
                    Console.Clear();
                    OverzichtProductLijst();
                    Console.WriteLine("\nGeef de naam in gevolgd door 'enter':");
                    userinput = Console.ReadLine();
                    Console.Clear();
                    OverzichtProductLijst("naam", userinput);
                    MenuProductZoeken();
                    break;
            }
        }

        public static void MenuProductEnkel(int productID, string zoekenToevoegen)
        {
            OverzichtProductEnkel(productID);

            Console.WriteLine($"1.Product aanpassen\n2.Bestellingen tonen\n3.Terug");
            switch (Menu.MethodeKiezer(3))
            {
                case 1:
                    Console.Clear();
                    OverzichtProductEnkel(productID);
                    MenuProductEdit(productID);
                    Console.Clear();
                    MenuProductEnkel(productID, zoekenToevoegen);
                    break;

                case 2:
                    Console.Clear();
                    OverzichtProductBestellingen(productID);
                    Console.WriteLine($"1.Bestelling selecteren\n2.Terug");
                    switch (Menu.MethodeKiezer(2))
                    {
                        case 1:
                            Console.WriteLine("TODO");                                                  ///////////// TODO > GO TO MAIN BESTELMENU
                            Console.ReadLine();
                            break;

                        case 2:
                            break;
                    }
                    Console.Clear();
                    MenuProductEnkel(productID, zoekenToevoegen);
                    break;

                case 3:
                    if (zoekenToevoegen == "zoeken")
                    {
                        Console.Clear();
                        OverzichtProductLijst();
                        MenuProductZoeken();
                    }
                    else if (zoekenToevoegen == "toevoegen")
                    {
                        Console.Clear();
                        MenuProductHoofdmenu();
                    }

                    break;
            }
        }

        public static void MenuProductEdit(int productID)
        {
            List<Product> productEdit = Data.GetProduct();

            Console.WriteLine
                ($"1.Verwijder\\herstel product\n2.Terug\n\nWijzig: \n3.naam\n4.prijs\n5.voorraad\n6.status\n");

            switch (Menu.MethodeKiezer(6))
            {
                case 1:
                    productEdit[productID - 1].Actief = !productEdit[productID - 1].Actief;
                    break;

                case 2:
                    Console.Clear();
                    MenuProductEnkel(productID, "zoeken");
                    break;

                case 3:
                    Console.Clear();
                    OverzichtProductEnkel(productID);
                    Console.WriteLine("Geef de nieuwe naam in:");
                    productEdit[productID - 1].Naam = Console.ReadLine();
                    break;

                case 4:
                    Console.Clear();
                    OverzichtProductEnkel(productID);
                    Console.WriteLine("Geef de nieuwe waarde in:");
                    productEdit[productID - 1].Prijs = Convert.ToDecimal(Console.ReadLine());
                    break;

                case 5:
                    Console.Clear();
                    OverzichtProductEnkel(productID);
                    Console.WriteLine("Geef de nieuwe voorraad in:");
                    productEdit[productID - 1].Voorraad = Convert.ToInt32(Console.ReadLine());
                    break;

                case 6:
                    Console.Clear();
                    OverzichtProductEnkel(productID);
                    Console.WriteLine("Verander hier de status van het product: TODO");
                    //productEdit[productID - 1].HuisBusNummer = Console.ReadLine();
                    break;
            }

            Data.SetProduct(productEdit);
            Console.Clear();
            OverzichtProductEnkel(productID);
            MenuProductEdit(productID);
        }

        public static void OverzichtProductBestellingen(int ProductID)
        {

            //Roep gezoon bestellingen op ipv deze methode, voorbeeld voor product met ID 5:  OverzichtProductLijst("product",  5)
            Console.WriteLine("To DELETE");
        }

        public static void OverzichtProductEnkel(int productID)
        {
            Product objectSelectie = Data.GetProduct().Find(delegate (Product del) { return del.ID == productID; });

            if (!objectSelectie.Actief)
            {
                Console.WriteLine($"\n\tLET OP: DIT PRODUCT WERD VERWIJDERD");
            }
            Console.WriteLine($"\n\tID: {objectSelectie.ID}\n\tNaam: {objectSelectie.Naam} \nPrijs: {objectSelectie.Prijs}\n\tVoorraad: {objectSelectie.Voorraad}\n\t");
        }

        public static void OverzichtProductLijst(string zoekmethode = "alles", string parameter = "alles")
        {
            List<Product> results = Data.GetProduct();
            switch (zoekmethode)
            {
                case "ID":
                    results = Data.GetProduct().FindAll(x => x.ID == Int32.Parse(parameter.ToLower()));
                    break;

                case "Naam":
                    results = Data.GetProduct().FindAll(x => x.Naam.ToLower() == parameter.ToLower());
                    break;

                case "Prijs":
                    results = Data.GetProduct().FindAll(x => x.Prijs == Convert.ToDecimal(parameter));
                    break;

                case "alles":
                    break;
            }
            Console.WriteLine(" |ID  " + $"|Naam".PadRight(12) + $"|Prijs".PadRight(12) + $"|Voorraad".PadRight(20) + $"ID".PadRight(10) + $"|Status".PadRight(20) + $"|Aanmaakdatum ");

            foreach (var item in results)
            {
                Console.WriteLine($" |{item.ID}".PadRight(6) + $"|{item.Naam}".PadRight(12) + $"|{item.Prijs}".PadRight(12) + $"|{item.Voorraad}".PadRight(20) + $"|{item.ID}".PadRight(10) + $"|{item.Actief}".PadRight(20) + $"|{item.DatumAanmaak}");
            }
        }

        public static void MethodeProductToevoegen()
        {
            Product foo = new Product("bar");
            foo.MethodeNieuwProduct();
            Data.AddProduct(foo);
            MenuProductEnkel(foo.ID, "toevoegen");
        }
    }
}