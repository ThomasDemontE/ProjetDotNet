using System;
using System.Device.Gpio;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Test_Button
{
    public class Program
    {
        //Definition des pins
        public static int LED_PIN = 18;
        public static int BTN_PIN = 19;
        public static void Main(string[] args)
        {
            bouton();
        }

        //Fonction pour ecouter les actions sur le bouton
        private static void bouton()
        {
            //Creation d'un controller pour gerer les inputs outputs
            using var gpioController = new GpioController();

            while (true)
            {
                //Lit la valeur du bouton
                gpioController.OpenPin(BTN_PIN, PinMode.Input);
                PinValue valeurBouton = gpioController.Read(BTN_PIN);
                gpioController.ClosePin(BTN_PIN);
                //Si le bouton est appuye
                if((valeurBouton == PinValue.Low))
                {
                    if (CallWebApi())
                        {
                            //Allume la LED
                            gpioController.OpenPin(LED_PIN, PinMode.Output);
                            gpioController.Write(LED_PIN, PinValue.High);
                            //Attend afin de ne pas spammer le serveur
                            Thread.Sleep(500);
                            gpioController.ClosePin(LED_PIN);

                            Console.WriteLine("Debug sonette High");
                        }
                        else
                        {
                            //Eteint la LED
                            gpioController.OpenPin(LED_PIN, PinMode.Output);
                            gpioController.Write(LED_PIN, PinValue.Low);
                            //Attend afin de ne pas spammer le serveur
                            Thread.Sleep(500);
                            gpioController.ClosePin(LED_PIN);

                            Console.WriteLine("Debug sonette Low");
                        }
                } 
            }
        }

        /**
         * Fonction pour envoyer un message au serveur
         */
        private static bool CallWebApi()
        {
            //Instancie un objet permettant d'envoyer des messages http vers un serveur
            HttpClient client = new HttpClient();

            //Definition d'une variable afin de stocker le message, ce dernier est vide (pas de data)
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,//Type get
                RequestUri = new Uri("http://192.168.1.14:5001"), //Changer en fonction de l'adresse IP du serveur
            };

            //Envoi le message
            HttpResponseMessage message = client.Send(request);
            //Renvoi si le message a ete accepte par le serveur ou non
            return message.IsSuccessStatusCode;
            }
        }
    }
