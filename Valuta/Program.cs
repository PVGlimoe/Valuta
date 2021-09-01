using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Xml.XPath;

namespace Valuta
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter amount in DKK");
            string amountInDkk =  Console.ReadLine();
            Console.WriteLine("Enter currency code");
            string codeChosen = Console.ReadLine();

            XDocument doc = XDocument.Load("https://www.nationalbanken.dk/_vti_bin/DN/DataService.svc/CurrencyRatesXML?lang=da%22");

            var currencies = doc.XPathSelectElements("//currency");

            List<Currency> listOfCurrencies = new List<Currency>();

            foreach (XElement element in currencies)
            {

                string code = element.Attribute("code").Value;

                string desc = element.Attribute("desc").Value;

                decimal rate = Decimal.Parse(element.Attribute("rate").Value);

                Currency currency = new Currency();

                currency.Code = code;
                currency.Desc = desc;
                currency.Rate = rate;

                listOfCurrencies.Add(currency);

            }

            for (int i = 0; i < listOfCurrencies.Count; i++)
            {
                if (codeChosen.Equals(listOfCurrencies[i].Code))
                {
                    decimal exchangedAmount = Decimal.Parse(amountInDkk) / (listOfCurrencies[i].Rate/100);
                    Console.WriteLine("For " + amountInDkk + " you get " + exchangedAmount + " " + listOfCurrencies[i].Code + " ~~ " + listOfCurrencies[i].Desc);
                    return;
                }

            }
        }
    }

    class Currency
    {
       private string code;
        public string Code
        {
            get { return code; }
            set { code = value; }
        }
       private string desc;
        public string Desc
        {
            get { return desc; }
            set { desc = value; }
        }

        private decimal rate;
        public decimal Rate
        {
            get { return rate; }
            set { rate = value; }
        }
    }

}
