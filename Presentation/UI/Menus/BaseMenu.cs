namespace Presentation.UI.Menus
{
    internal abstract class BaseMenu
    {
        protected int SelectedIndex;
        protected string[] Options;
        protected string Prompt;

        protected BaseMenu(string prompt, string[] options)
        {
            Prompt = prompt;
            Options = options;
            SelectedIndex = 0;
        }

        protected void DisplayOptions()
        {
            Console.Clear();
            Console.WriteLine(Prompt);

            for (int i = 0; i < Options.Length; i++)
            {
                string currentOption = Options[i];
                string prefix;

                if (i == SelectedIndex)
                {
                    prefix = "*";
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.White;
                }
                else
                {
                    prefix = " ";
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.Black;
                }
                Console.WriteLine($"{prefix} << {currentOption} >>");
            }
            Console.ResetColor();
        }

        public virtual int Run()
        {
            ConsoleKey keyPressed;

            do
            {
                DisplayOptions();

                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                keyPressed = keyInfo.Key;

                // Update SelectedIndex based on arrow keys.
                if (keyPressed == ConsoleKey.UpArrow)
                {
                    SelectedIndex--;
                    if (SelectedIndex == -1)
                        SelectedIndex = Options.Length - 1;
                }
                else if (keyPressed == ConsoleKey.DownArrow)
                {
                    SelectedIndex++;
                    if (SelectedIndex == Options.Length)
                        SelectedIndex = 0;
                }
                else if (keyPressed == ConsoleKey.Escape)
                {
                    return -1;
                }
            } while (keyPressed != ConsoleKey.Enter);

            return SelectedIndex;
        }

        protected void WaitForKey(string? message = null)
        {
            if (!string.IsNullOrWhiteSpace(message))
                Console.WriteLine(message);

            Console.ReadKey(true);
        }

        protected void DisplayTitle(string title)
        {
            Console.Clear();
            Console.WriteLine($"======== {title} ========\n");
        }

        protected ConsoleKey WaitForEnterOrEscape()
        {
            while (true)
            {
                ConsoleKey key = Console.ReadKey(true).Key;

                if (key == ConsoleKey.Enter || key == ConsoleKey.Escape)
                    return key;
            }
        }
    }
}
