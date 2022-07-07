using System;
using System.Linq;
using System.Collections.Generic;

namespace Energy_Smoothie
{ 
    class Program
    {
        static void Main(string[] args)
        {
            List<string> fruits = new List<string>() { "Apple", "Banana", "Pineapple", "Watermelon", "Strawberry" };

            Welcome_Message(fruits);
            Console.ReadLine();
        }

        /// <summary>
        /// Receives a list {fruits} as input and returns 
        /// a string{fruit} chosen from the list.
        /// </summary>
        /// <param name="fruits"></param>
        /// <returns name="fruit"></returns>

        public static string Pick_Fruit(List<string> fruits, Dictionary<string, int> blender_content)
        {
            Console.WriteLine("Choose a fruit by typing \"{fruit's name}\"");

            // Print out the item in the fruits list in one line.
            Console.WriteLine($" ==> { string.Join(" , ", fruits)} <==");

            Console.Write("Fruit: ");

            string fruit = Console.ReadLine();

            Confirm_Fruit(fruit, fruits, blender_content);

            return fruit;

        }

        public static void Welcome_Message(List<string> fruits)
        {
            Console.Write("Please enter your name: ");
            string chef_name = Console.ReadLine();
            Dictionary<string, int> blender_content = new Dictionary<string, int>();

            string message = "Hello " + chef_name + "  I'm glad you decided to hop o this!\nYour task is to make an energy fruit smoothie. \nYou would be offered 5 fruits and you must make use of a minimum of 2 fruits. \nType \"Ready\" to start";
            Console.WriteLine(message);

            string ready = Console.ReadLine();

            if (ready == "Ready")
            {
                Pick_Fruit(fruits, blender_content);
            }
            else
            {
                Console.WriteLine("Please type \"Ready\" to start the competition");
            }
        }

        /// <summary>
        /// Confirm if the chosen fruit is in the list
        /// and displays the fruit varieties
        /// </summary>
        /// <param name="fruit"></param>
        /// <returns name="fruits">initial list of fruits minus the chosen fruit</returns>

        static List<string> Confirm_Fruit(string fruit, List<string>fruits, Dictionary<string, int> blender_content)
        {
            //List<string> fruits = new List<string>() { "Apple", "Banana", "Pineapple", "Watermelon", "Strawberry" };

            if (fruits.Contains(fruit))
            {
                Console.WriteLine("Please choose a/an " + fruit + " variety");
                switch (fruit)
                {
                    case "Apple":
                        Console.WriteLine("==> Sliced Apple, Chopped Apple <==");
                        break;

                    case "Banana":
                        Console.WriteLine("==> Sliced Banana, Mashed Banana <==");
                        break;

                    case "Pineapple":
                        Console.WriteLine("==> Sliced Pineapple, Pineapple Chunks <==");
                        break;

                    case "Watermelon":
                        Console.WriteLine("==> Diced Watermelon, Watermelon Balls <==");
                        break;

                    case "Strawberry":
                        Console.WriteLine("==> Sliced Strawberry, Strawberry Puree <==");
                        break;

                    default:
                        Console.WriteLine("Please pick a fruit from the list");
                        break;
                }

            }
            else
            {
                Pick_Fruit(fruits, blender_content);
            }

            /// <summary>
            /// Recieve the fruit variety input,
            /// removes fruit type from the list of fruits and returns the new fruit list,
            /// </summary>
            string fruits_variety = Console.ReadLine();

            Chosen_Fruits(fruits_variety, fruit, fruits, blender_content);
            Pick_Fruit(fruits, blender_content);

            return (fruits);
        }

        /// <summary>
        /// Check if the chosen fruit variety is a key in the fruit dictionary
        /// </summary>
        /// <param name="fruits_variety"></param>
        /// <param name="fruit"></param>
        /// <returns>a dictionary of the chosen fruit variety and the calorie value</returns>
        static Dictionary<string, int> Chosen_Fruits(string fruits_variety, string fruit, List<string> fruits, Dictionary<string, int> blender_content)
        {
            Dictionary<string, int> fruit_dictionary = new Dictionary<string, int>();

            fruit_dictionary.Add("Sliced Apple", 57);
            fruit_dictionary.Add("Chopped Apple", 65);
            fruit_dictionary.Add("Sliced Banana", 133);
            fruit_dictionary.Add("Mashed Banana", 200);
            fruit_dictionary.Add("Sliced Pineapple", 42);
            fruit_dictionary.Add("Pineapple Chunks", 82);
            fruit_dictionary.Add("Diced Watermelon", 46);
            fruit_dictionary.Add("Watermelon Balls", 37);
            fruit_dictionary.Add("Sliced Strawberry", 54);
            fruit_dictionary.Add("Strawberry Puree", 76);

            
            List<string> chosen_fruits = new List<string>();
            chosen_fruits.Add(fruits_variety);

            int chosen_fruit_calorie;

            if (fruit_dictionary.TryGetValue(fruits_variety, out chosen_fruit_calorie))
            {
                blender_content.Add(fruits_variety, chosen_fruit_calorie);
            }
            else
            {
                Confirm_Fruit(fruit,fruits, blender_content);
            }

            fruits.Remove(fruit);
            Can_Blend(blender_content,fruit,fruits);

            return (blender_content);
        }

        /// <summary>
        /// Checks if the chef can now blend the fruits to make the smoothie
        /// </summary>
        /// <param name="blender_content"></param>
        /// <param name="fruit"></param>
        /// <param name="fruits"></param>
        static void Can_Blend(Dictionary<string, int> blender_content, string fruit, List<string> fruits)
        {
            if (blender_content.Count >= 2 && blender_content.Count < 5)
            {
                Console.WriteLine("You now have the option to mix your smoothie \nType \"Blend\" to make your smothie or type \"Add\" to add more fruits");

                string blend = Console.ReadLine();

                switch (blend)
                {
                    case "Blend":
                        Calorie_Calculator(blender_content);
                        System.Environment.Exit(0);
                        break;
                    case "Add":
                        Confirm_Fruit(fruit,fruits, blender_content);
                        break;
                    default:
                        Console.WriteLine("\nType \"Blend\" to make your smothie or type \"Add\" to add more fruits");
                        break;
                }
            }
            
            else if (blender_content.Count == 5)
            {
                Console.WriteLine("You've exceeded the fuit limit, please type \"Blend\" to make your smoothie");
                string blend = Console.ReadLine();
                
                if (blend == "Blend")
                {
                    Calorie_Calculator(blender_content);
                    System.Environment.Exit(0);
                }
                else
                {
                    Can_Blend(blender_content, fruit, fruits);
                    System.Environment.Exit(0);
                }
            }

        }

        /// <summary>
        /// Calculate the amount of calories in the smoothie
        /// </summary>
        /// <param name="blender_content"></param>
        static void Calorie_Calculator(Dictionary<string, int> blender_content)
        {
            Console.Write("Give your smoothie a name: ");
            string smoothie_name = Console.ReadLine();
            Console.WriteLine(smoothie_name + " is ready");

            var total = blender_content.Sum(x => x.Value);

            if (total < 200)
            {
                Console.WriteLine(smoothie_name + " is a low energy smoothie");

            }
            else if (total >= 200) {
                Console.WriteLine(smoothie_name + " is a perfect energy smoothie");
            }
            else if(total > 500)
            {
                Console.WriteLine("Haba! " + smoothie_name + " has too much energy");
            }

            foreach (KeyValuePair<string, int> element in blender_content)
            {
                Console.WriteLine("{0} ==> {1} cal",
                          element.Key, element.Value);
            }
            Console.WriteLine("Total number of calories in your smoothie is: " + total + " Cal");
        }
    }

}
