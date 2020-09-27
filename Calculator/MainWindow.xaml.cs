﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Calculator
{
    /// <summary>
    /// An enumeration representing the operations this 
    /// calculator can employ
    /// </summary>
    enum Operation 
    {
        None = 0,
        Add = 1, 
        Subtract = 2,
        Multiply = 3,
        Divide = 4
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // An array to hold the operands
        int[] operands = new int[2];

        // The in-progress operation
        Operation operation = Operation.None;

        // Determines if the next number key overwrites the display
        bool nextKeyOverwrites = true;

        /// <summary>
        /// Constructs and initializes the window
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Clears the display and cached values, putting the 
        /// calculator into an inital state
        /// </summary>
        /// <param name="sender">The button clicked</param>
        /// <param name="e">The event args</param>
        void onClear(object sender, RoutedEventArgs e)
        {
            operation = Operation.None;
            display.Text = "0";
            nextKeyOverwrites = true;
        }

        /// <summary>
        /// Handles the click of a "number" button
        /// </summary>
        /// <param name="sender">The button clicked</param>
        /// <param name="e">The event arguements</param>
        void onNumberPress(object sender, RoutedEventArgs e)
        {
            if(sender is Button button)
            {
                // Check to see if the display currenlty shows a zero
                // or if we are supposed to overwrite the display
                if (display.Text == "0" || nextKeyOverwrites)
                {
                    // If it does, replace the zero with the entered number
                    display.Text = button.Tag.ToString();
                    // and turn off the "nextKeyOverwrites"
                    nextKeyOverwrites = false;
                }
                else
                {
                    // Otherwise, add the number as an additional place
                    display.Text += button.Tag;
                }
            }
        }

        /// <summary>
        /// Handles the click of an "operator" button (i.e. +)
        /// </summary>
        /// <param name="sender">The button clicked</param>
        /// <param name="e">The event parameters</param>
        void onOperatorPress(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                // Determine if we are already performing an operation
                if (operation != Operation.None)
                {
                    // If we are, save the current display value as the second operand
                    operands[1] = int.Parse(display.Text);
                    // Apply the cached operation 
                    applyOperation();
                }
                else
                {
                    // Otherwise, cache the current display value as the first operand
                    operands[1] = int.Parse(display.Text);
                    // Clear the display 
                    display.Text = "0";
                }
                // And cache the new operation
                operation = (Operation)int.Parse(button.Tag.ToString());
            }
        }

        /// <summary>
        /// Applies the currently cached operation to the 
        /// cached operands, and displays the results 
        /// </summary>
        void applyOperation()
        {
            // Apply the cached operation to the cached operands,
            // and display the result
            switch(operation)
            {
                case Operation.Add:
                    display.Text = (operands[0] + operands[1]).ToString();
                    break;
                case Operation.Subtract:
                    display.Text = (operands[0] - operands[1]).ToString();
                    break;
                case Operation.Multiply:
                    display.Text = (operands[0] * operands[1]).ToString();
                    break;
                case Operation.Divide:
                    display.Text = (operands[0] / operands[1]).ToString();
                    break;
            }
            // Clear the cached operand
            operation = Operation.None;
        }
    }
}
