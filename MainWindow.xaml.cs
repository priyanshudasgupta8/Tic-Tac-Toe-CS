using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Tic_Tac_Toe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Private Members   

 
        // Holds the current results of the cells in the active game
        private MarkType[] mResults;

        // Returns true if it is that player's turn
        private bool mPlayer1Turn;

        // Returns true if game has ended
        private bool mGameEnded;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            NewGame();
        }

        #endregion

        /// <summary>
        /// Starts a new game and clears all vars back to the default (start)
        /// </summary>
        private void NewGame()
        {
            // Creates the array for the cells
            mResults = new MarkType[9];

            for (var i = 0; i < mResults.Length; i++) 
                mResults[i] = MarkType.Free;

            // Make sure that Player 1 = current player
            mPlayer1Turn = true;

            // Iterate over every button on the grid
            Container.Children.Cast<Button>().ToList().ForEach(button => {
                // Changes the stylings
                button.Content = string.Empty;
                button.Background = Brushes.Gray;
                button.Foreground = Brushes.Cyan;
            });

            // Make sure the game hasn't finished
            mGameEnded = false;
        }

        /// <summary>
        /// Handles a button click event 
        /// </summary>
        /// <param name="sender">The button that was clicked</param>
        /// <param name="e">The events of the click</param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Start a new game after it is finished    
            if (mGameEnded)
            {
                NewGame();
                return;
            }

            // Cast and find the index of the button
            var button = (Button)sender;
            var column = Grid.GetColumn(button);
            var row = Grid.GetRow(button);

            var index = column + (row * 3);

            // Don't do anything if button is occupied
            if (mResults[index] != MarkType.Free)
                return;

            // Set the cell value and text based on whose turn it is AND switch the value and text
            mResults[index] = mPlayer1Turn ? MarkType.Cross : MarkType.Nought;
            button.Content = mPlayer1Turn ? "X" : "O";
            
            if (!mPlayer1Turn)
                button.Foreground = Brushes.Aquamarine;

            mPlayer1Turn ^= true;

            // Check for a winner
            CheckForWinner();
        }

        /// <summary>
        /// Checks if there is a winner in the game
        /// </summary>
        private void CheckForWinner()
        {
            // Checking done manually :(
            // Check for horizontal wins
            if (mResults[0] != MarkType.Free && (mResults[1] & mResults[2]) == mResults[0])
            {
                mGameEnded = true;
                Button0_0.Background = Button1_0.Background = Button2_0.Background = Brushes.LightGreen;
            } else if (mResults[3] != MarkType.Free && (mResults[4] & mResults[5]) == mResults[3])
            {
                mGameEnded = true;
                Button0_1.Background = Button1_1.Background = Button2_1.Background = Brushes.LightGreen;
            } else if (mResults[6] != MarkType.Free && (mResults[7] & mResults[8]) == mResults[6])
            {
                mGameEnded = true;
                Button0_2.Background = Button1_2.Background = Button2_2.Background = Brushes.LightGreen;
            }

            // Check for vertical wins
            if (mResults[0] != MarkType.Free && (mResults[3] & mResults[6]) == mResults[0])
            {
                mGameEnded = true;
                Button0_0.Background = Button0_1.Background = Button0_2.Background = Brushes.LightGreen;
            }
            else if (mResults[1] != MarkType.Free && (mResults[4] & mResults[7]) == mResults[1])
            {
                mGameEnded = true;
                Button1_0.Background = Button1_1.Background = Button1_2.Background = Brushes.LightGreen;
            }
            else if (mResults[2] != MarkType.Free && (mResults[5] & mResults[8]) == mResults[2])
            {
                mGameEnded = true;
                Button2_0.Background = Button2_1.Background = Button2_2.Background = Brushes.LightGreen;
            }

            // Check for diagonal wins
            if (mResults[0] != MarkType.Free && (mResults[4] & mResults[8]) == mResults[0])
            {
                mGameEnded = true;
                Button0_0.Background = Button1_1.Background = Button2_2.Background = Brushes.LightGreen;
            }
            else if (mResults[2] != MarkType.Free && (mResults[4] & mResults[6]) == mResults[2])
            {
                mGameEnded = true;
                Button0_2.Background = Button1_1.Background = Button2_0.Background = Brushes.LightGreen;
            }

            // Check for tie
            if (!mResults.Any(result => result == MarkType.Free))
            {
                mGameEnded = true;
                Container.Children.Cast<Button>().ToList().ForEach(button => {
                    button.Background = Brushes.DarkOrange;
                });
            }
        }
    }
}
