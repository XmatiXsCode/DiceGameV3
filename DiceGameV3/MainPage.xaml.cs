namespace DiceGameV3
{
    public partial class MainPage : ContentPage
    {
        int healthLevel = 100;
        int experianceLevel = 0;
        int radioactivityLevel = 0;
        bool firstquest = false;
        bool secondquest = false;
        bool thirdtest = false;
        List<int> assigments = new List<int> { 0, 0, 0, 0, 0, 0 };
        List<string> assigmentsList = new List<string> { "", "", "" };
        public MainPage()
        {
            InitializeComponent();

            MakeAssigmentsAndStart();
        }
        public void Roll(object sender, EventArgs e)
        {
            playButton.Text = "Przetasuj kości";
            playButton.BackgroundColor = Colors.LightGray;
            healthLevel = healthLevel - radioactivityLevel + experianceLevel;
            if (healthLevel > 100)
            {
                healthLevel = 100;
            }

            if (healthLevel < 1 || firstquest && secondquest && thirdtest)
            {
                MakeAssigmentsAndStart();
                return;
            }

            List<int> draws = new List<int> { 0, 0, 0, 0, 0 };
            for (int i = 0; i < draws.Count(); i++)
            {
                draws[i] = new Random().Next(1, 7);
            }
            firstDice.Source = $"kostka{draws[0]}.png";
            secondDice.Source = $"kostka{draws[1]}.png";
            thirdDice.Source = $"kostka{draws[2]}.png";
            fourthDice.Source = $"kostka{draws[3]}.png";
            fifthDice.Source = $"kostka{draws[4]}.png";
            List<int> drawInDraws = new List<int> { 0, 0, 0, 0, 0, 0 };
            for (int i = 0; i < draws.Count(); i++)
            {
                for (int j = 0; j < drawInDraws.Count(); j++)
                {
                    if (draws[i] == j + 1)
                    {
                        drawInDraws[j]++;
                    }
                }
            }
            for (int i = 0; i < drawInDraws.Count(); i++)
            {
                if (drawInDraws[i] > 1)
                {
                    radioactivityLevel = radioactivityLevel + (drawInDraws[i] - 1);
                }
                if (drawInDraws[i] > 2)
                {
                    experianceLevel = experianceLevel + drawInDraws[i];
                }
            }
            health.Text = $"Pozostało zdrowia:\n{healthLevel}";
            if (experianceLevel > 0)
            {
                experiance.Text = $"Poziom doświadczenia:\n{experianceLevel}";
                experiance.Background = Colors.Blue;
            }
            if (radioactivityLevel > 0)
            {
                radioactivity.Text = $"Poziom radioaktywności:\n{radioactivityLevel}";
                radioactivity.Background = Colors.Yellow;
                radioactivity.TextColor = Colors.Black;
            }

            if (assigments[0] == assigments[1])
            {
                if (drawInDraws[assigments[0] - 1] > 1)
                {
                    assigmentsList[0] = $"- 2x {assigments[0]} (zrobione)";
                    firstquest = true;
                }
            }
            else
            {
                if (drawInDraws[assigments[0] - 1] > 0 && drawInDraws[assigments[1] - 1] > 0)
                {
                    assigmentsList[0] = $"- {assigments[0]} i {assigments[1]} (zrobione)";
                    firstquest = true;
                }
            }

            if (assigments[2] == assigments[3])
            {
                if (drawInDraws[assigments[2] - 1] > 2)
                {
                    assigmentsList[1] = $"- 3x {assigments[2]} (zrobione)";
                    secondquest = true;
                }
            }
            else
            {
                if (drawInDraws[assigments[2] - 1] > 1 && drawInDraws[assigments[3] - 1] > 0)
                {
                    assigmentsList[1] = $"- 2x {assigments[2]} i {assigments[3]} (zrobione)";
                    secondquest = true;
                }
            }

            if (assigments[4] == assigments[5])
            {
                if (drawInDraws[assigments[4] - 1] > 3)
                {
                    assigmentsList[2] = $"- 4x {assigments[4]} (zrobione)";
                    thirdtest = true;
                }
            }
            else
            {
                if (drawInDraws[assigments[4] - 1] > 2 && drawInDraws[assigments[5] - 1] > 0)
                {
                    assigmentsList[2] = $"- 3x {assigments[4]} i {assigments[5]} (zrobione)";
                    thirdtest = true;
                }
            }
            periodicRequest.Text = $"Wymagane pierwiastki:\n{assigmentsList[0]}\n{assigmentsList[1]}\n{assigmentsList[2]}";

            if (firstquest && secondquest && thirdtest)
            {
                playButton.BackgroundColor = Colors.Gold;
                periodicRequest.Text = $"WYGRANA\nStworzono pierwiastki:\n{assigmentsList[0]}, {assigmentsList[1]}, {assigmentsList[2]}.";
                playButton.Text = "Zagraj ponownie";
                return;
            }

            int temp = healthLevel;
            temp = temp - radioactivityLevel + experianceLevel;
            if (temp < 1)
            {
                playButton.BackgroundColor = Colors.Red;
                periodicRequest.Text = $"PRZEGRANA\nOto co udało się osiągnąć:\n{assigmentsList[0]}, {assigmentsList[1]}, {assigmentsList[2]}.";
                playButton.Text = "Zagraj ponownie";
            }
        }
        public void MakeAssigmentsAndStart()
        {
            for (int i = 0; i < 6; i++)
            {
                assigments[i] = new Random().Next(1, 7);
            }

            if (assigments[0] == assigments[1])
            {
                assigmentsList[0] = $"- 2x {assigments[0]}";
            }
            else
            {
                assigmentsList[0] = $"- {assigments[0]} i {assigments[1]}";
            }

            if (assigments[2] == assigments[3])
            {
                assigmentsList[1] = $"- 3x {assigments[2]}";
            }
            else
            {
                assigmentsList[1] = $"- 2x {assigments[2]} i {assigments[3]}";
            }

            if (assigments[4] == assigments[5])
            {
                assigmentsList[2] = $"- 4x {assigments[4]}";
            }
            else
            {
                assigmentsList[2] = $"- 3x {assigments[4]} i {assigments[5]}";
            }

            healthLevel = 100;
            experianceLevel = 0;
            radioactivityLevel = 0;
            firstquest = false;
            secondquest = false;
            thirdtest = false;
            experiance.Background = Colors.Red;
            radioactivity.Background = Colors.Red;
            radioactivity.TextColor = Colors.White;
            firstDice.Source = $"kostka0.png";
            secondDice.Source = $"kostka0.png";
            thirdDice.Source = $"kostka0.png";
            fourthDice.Source = $"kostka0.png";
            fifthDice.Source = $"kostka0.png";
            health.Text = "Pozostało zdrowia:\n100";
            experiance.Text = "Poziom doświadczenia:\nniedostępne";
            radioactivity.Text = "Poziom radioaktywności:\nniedostępne";
            periodicRequest.Text = $"Wymagane pierwiastki:\n{assigmentsList[0]}\n{assigmentsList[1]}\n{assigmentsList[2]}";
        }
    }
}