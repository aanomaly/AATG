/* Autor:
 * Maciej Kukawski
 */

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;


namespace Markow20
{

    // ReSharper disable LocalizableElement

    public sealed partial class Form1 : Form
    {
        //Forward, right, down, left
        private static readonly double[] Probs = {0.8, 0.1, 0.0, 0.1};


        private const int ButtonSize = 50;
        private const int ButtonAndMarginSize = 60;
        private const double Epsilon = 0.001;
        private const int EvaluationIterationLimit = 20;

        private double r = -0.04;
        private double d = 1;
        private bool hasStart;
        private int x = 4;
        private int y = 3;

        private static readonly Color BlockedColor = Color.Black;
        private static readonly Color TerminalColor = Color.DarkViolet;
        private static readonly Color StartColor = Color.Blue;
        private static readonly Color NonTerminalColor = Color.Silver;


        public Form1()
        {
            InitializeComponent();
            AutoScroll = true;
            CreateList();
            buttons[4].BackColor = BlockedColor;
            buttons[9].BackColor = TerminalColor;
            buttons[10].BackColor = TerminalColor;
            buttons[9].ForeColor = Color.White;
            buttons[10].ForeColor = Color.White;
            buttons[9].Text = "1";

            buttons[10].Text = "-1";
            gammaLabel.Text = "\u03B3";

        }

        private readonly List<Button> buttons = new List<Button>();
        
        private Button CreateBoardButton(int bx, int by)
        {
            Button res = new Button
                             {
                                 BackColor = NonTerminalColor,
                                 Location = new Point(bx, @by),
                                 Name = "button" + (bx*y + @by + 9),
                                 Size = new Size(ButtonSize, ButtonSize),
                                 TabIndex = bx*y + @by + 9,
                                 UseVisualStyleBackColor = false
                             };
            res.MouseUp += CreateHandl(res);
            res.Tag = "0";
            Controls.Add(res);
            return res;
        }

        private void CreateList()
        {
            foreach (var button in buttons)
            {
                Controls.Remove(button);
            }
            buttons.Clear();
            int currentX = 0;
            for (int i = 0; i < x; i++)
            {
                int currentY = 120;
                for (int j = 0; j < y; j++)
                {
                    buttons.Add(CreateBoardButton(currentX,currentY));
                    currentY += ButtonAndMarginSize;
                }
                currentX += ButtonAndMarginSize;
            }
            Width =x*ButtonAndMarginSize;
            if (Width < 525)
            {
                Width = 525;
            }
            Height = 200+(y*ButtonAndMarginSize);
            hasStart = false;
        }

        private void ButtonClick(Button button)
        {
            if (button.BackColor == NonTerminalColor)
            {
                button.BackColor = TerminalColor;
                button.ForeColor = Color.White;
                button.Text = (string)button.Tag;
                //endstate
            }
            else if (button.BackColor == TerminalColor)
            {
                button.BackColor = BlockedColor;
                button.ForeColor = Color.Black;
                button.Tag = button.Text;
                button.Text = "";
                //blocking
            }
            else if (button.BackColor == BlockedColor)
            {
                if (!hasStart)
                {
                    button.BackColor = StartColor;
                    hasStart = true;
                }
                else
                {
                    button.BackColor = NonTerminalColor;
                }
            }
            else if (button.BackColor == StartColor)
            {
                hasStart = false;
                button.BackColor = NonTerminalColor;
            }            
        }

        public static string ShowDialog(string text, string caption, Button buttonToChange)
        {
            Form prompt = new Form {Width = 150, Height = 150, Text = ""};
            Label textLabel = new Label { Left = 30, Top = 20, Text = caption };
            TextBox textBox = new TextBox { Text = text, Left = 40, Top = 50, Width = 50 };
            Button confirmation = new Button { Text = "Ok", Left = 50, Width = 30, Top = 70 };
            confirmation.Click += (sender, e) =>
                                      {
                                          if (buttonToChange.BackColor == TerminalColor)
                                          {
                                              buttonToChange.Text = textBox.Text;
                                          }
                                          else
                                          {
                                              buttonToChange.Tag = textBox.Text;
                                          }

                                          prompt.Close(); };
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(textLabel);
            prompt.Controls.Add(textBox);
            prompt.ShowDialog();
            return textBox.Text;
        }

        public static void ShowError(string text)
        {
            Form prompt = new Form {Width = 200, Height = 150, Text = "Error"};
            Label textLabel = new Label { Left = 30, Top = 20, Text = text };
            Button confirmation = new Button { Text = "Ok", Left = 50, Width = 30, Top = 70 };
            confirmation.Click += (sender, e) => prompt.Close();
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(textLabel);
            prompt.ShowDialog();
        }



        private void Button4Click(object sender, EventArgs e)
        {
            CalculateStrategy();
        }

        private void XTrackBarScroll(object sender, EventArgs e)
        {
            x = xTrackBar.Value;
            xlabel.Text = "x = " + x;
        }

        private void YTrackBarScroll(object sender, EventArgs e)
        {
            y = yTrackBar.Value;
            ylabel.Text = "y = " + y;
        }

        MouseEventHandler CreateHandl(Button b1)
        {
            return (o,e) => MyMouseUp(e,b1);
        }

        private void MyMouseUp(MouseEventArgs e, Button b)
        {
            if (e.Button == MouseButtons.Left)
            {
                ButtonClick(b);
            }
            else
            {
                ShowDialog(b.BackColor == TerminalColor ? b.Text : (string) b.Tag, "Change Value", b);
            }
        }

        private void NewBoardButtonClick(object sender, EventArgs e)
        {
            CreateList();
        }

        private void CalculateStrategy()
        {
            string error = IsValidData();
            if (error != null)
            {
                ShowError(error);
                return;
            }
            List<State> states = CreateStates();
            List<double> utilities = CalculateUtilities(states);

            List<string> policy = CalculatePolicy(states, utilities);
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    Button b = buttons[i*y + j];
                    if (b.BackColor == Color.Blue || b.BackColor == Color.Silver)
                    {
                        b.Text = policy[i*y + j];
                    }
                }
            }
        }

        private string IsValidData()
        {
            float s;
            if (!float.TryParse(rTextbox.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out s))
            {
                return "Invalid R value";
            }
            if (!float.TryParse(gammaTextbox.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out s))
            {
                return "Invalid gamma value";
            }
            if (s <= 0 || s > 1)
            {
                return "Gamma should be 0 < gamma <= 1";
            }
            if (buttons.Count != x * y)
            {
                return "Error - not initialized board";
            }
          
            return null;
        }

        private void CalculateUtility()
        {
            string error = IsValidData();
            if (error != null)
            {
                ShowError(error);
                return;
            }
            List<State> states = CreateStates();
            List<double> utilities = CalculateUtilities(states);
            for (int i = 0; i < x*y; i++)
            {
                Button b = buttons[i];
                if (b.BackColor == StartColor || b.BackColor == NonTerminalColor)
                {
                    b.Text = "" + Math.Round(utilities[i],3).ToString(CultureInfo.InvariantCulture);
                }
            }
        }

        private void ClearButtons()
        {
            for (int i = 0; i < x * y; i++)
            {
                Button b = buttons[i];
                if (b.BackColor == StartColor || b.BackColor == NonTerminalColor)
                {
                    b.Text = "";
                }
            }
        }

        private List<string> CalculatePolicy(List<State> states, List<double> u)
        {
            List<int> policy = (new int[x*y].Select(_ => State.Randomizer.GiveRandomizer().Next(4))).ToList();
            bool unchanged;
            do
            {
                u = EvaluatePolicy(states, policy, u);
                unchanged = true;
                for (int i = 0; i < x * y; i++)
                {
                    if (states[i].Type == State.StateType.Blocked || states[i].Type == State.StateType.Terminal)
                    {
                        continue;
                    }
                    double max = double.NegativeInfinity;
                    int maxIndex = -1;
                    double chosen = 0;
                    for (int k = 0; k < states[i].Actions.Count; k++)
                    {
                        var action = states[i].Actions[k];
                        double sum = action.State.Select(t => u[t.Index]).Select((us, j) => (us*action.Prob[j])).Sum();
                        if (k == policy[i])
                        {
                            chosen = sum;
                        }
                        if (sum > max)
                        {
                            max = sum;
                            maxIndex = k;
                        }
                    }
                    if (max > chosen)
                    {
                        policy[i] = maxIndex;
                        unchanged = false;
                    }
                }
            } while (!unchanged);

            return policy.Select(a =>
                                     {
                                         switch (a)
                                         {
                                             case 0:
                                                 return "\u25B2";
                                             case 1:
                                                 return "\u25B6";
                                             case 2:
                                                 return "\u25BC";
                                             case 3:
                                                 return "\u25C0";
                                             default:
                                                 throw new Exception();
                                         }
                                     }).ToList();
        }


        private List<double> EvaluatePolicy(List<State> states, List<int> policy, List<double> u )
        {

            for (int k = 0; k < EvaluationIterationLimit; k++){
                for (int i = 0; i < x * y; i++)
                {
                    State s = states[i];
                    if (s.Type == State.StateType.Blocked || s.Type == State.StateType.Terminal)
                    {
                        continue;
                    }
                    double res = 0;
                    Action action = s.Actions[policy[i]];
                    for (int j = 0; j < action.Prob.Count; j++)
                    {
                        double us = u[action.State[j].Index];
                        res += action.Prob[j] * us;
                    }
                    u[i] = s.Reward + d * res;
                }
            }
            return u;
        }

        private List<double> CalculateUtilities(List<State> states)
        {
            List<double> u = new List<double>();
            List<double> u1 = new List<double>();
            double delta;
            for (int i = 0; i < x * y; i++)
            {
                u.Add(states[i].Type == State.StateType.Terminal ? states[i].Reward : 0);
                u1.Add(states[i].Type == State.StateType.Terminal ? states[i].Reward : 0);
            }
            do
            {
                delta = 0;
                u = new List<double>(u1);
                for (int i = 0; i < x*y; i++)
                {
                    State s = states[i];
                    if (s.Type == State.StateType.Blocked || s.Type == State.StateType.Terminal)
                    {
                        continue;
                    }
                    double max = double.NegativeInfinity;
                    foreach (var action in s.Actions)
                    {
                        double res = 0;
                        for (int j = 0; j < action.Prob.Count; j++ )
                        {
                            double us = u[action.State[j].Index];
                            res += action.Prob[j] * us;
                        }
                        if (res > max)
                        {
                            max = res;
                        }
                    }
                    u1[i] = s.Reward + d * max;
                    if (Math.Abs(u1[i] - u[i]) > delta)
                    {
                        delta = Math.Abs(u1[i] - u[i]);
                    }
                }
            } while (delta > (Epsilon*(1 - d)/d));
            return u;
        }

        #region CreateStates
        private List<State> CreateStates()
        {
            List<State> states = new List<State>();
            SetTypesAndRewards(states);
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    int currentIndex = i * y + j;
                    State currentState = states[currentIndex];
                    if (currentState.Type == State.StateType.Terminal || currentState.Type == State.StateType.Blocked)
                    {
                        Action a = new Action();
                        a.Prob.Add(1.0);
                        a.State.Add(currentState);
                        currentState.Actions.Add(a);
                        continue;
                    }
                    CreateUpAction(states, currentIndex, currentState);
                    CreateRightAction(states, currentIndex, currentState);
                    CreateDownAction(states, currentIndex, currentState);
                    CreateLeftAction(states, currentIndex, currentState);
                }
            }
            return states;
        }

        /*
         *  <- -y
         *  -> +y
         *  ^ -1
         *  V +1
         */

        private void CreateLeftAction(List<State> states, int currentIndex, State currentState)
        {
            int[] indexes = new int[4];
            indexes[0] = currentIndex - y;
            indexes[1] = currentIndex % y == 0 ? -1 : (currentIndex - 1);
            indexes[2] = currentIndex + y;
            indexes[3] = currentIndex + 1;
            CreateAction(currentState, states, indexes, currentIndex);
        }

        private void CreateUpAction(List<State> states, int currentIndex, State currentState)
        {
            int[] indexes = new int[4];
            indexes[0] = currentIndex % y == 0 ? -1 : (currentIndex - 1); 
            indexes[1] = currentIndex + y;
            indexes[2] = currentIndex + 1;
            indexes[3] = currentIndex - y;
            CreateAction(currentState, states, indexes,currentIndex);
        }

        private void CreateDownAction(List<State> states, int currentIndex, State currentState)
        {
            int[] indexes = new int[4];
            indexes[0] = currentIndex + 1;
            indexes[1] = currentIndex - y;
            indexes[2] = currentIndex % y == 0 ? -1 : (currentIndex - 1); 
            indexes[3] = currentIndex + y;
            CreateAction(currentState, states, indexes,currentIndex);
        }

        private void CreateRightAction(List<State> states, int currentIndex, State currentState)
        {
            int[] indexes = new int[4];
            indexes[0] = currentIndex + y;
            indexes[1] = currentIndex + 1;
            indexes[2] = currentIndex - y;
            indexes[3] = currentIndex % y == 0 ? -1 : (currentIndex - 1); 
            CreateAction(currentState, states, indexes, currentIndex);
        }

        private void CreateAction(State currentState, List<State> states, int[]indexes, int currentIndex)
        {
            Action action = new Action();
            for (int i = 0; i < 4; i++)
            {
                action.Prob.Add(Probs[i]);
                if (!IsInField(indexes[i], currentIndex) || states[indexes[i]].Type == State.StateType.Blocked)
                {
                    action.State.Add(currentState);
                }
                else
                {
                    action.State.Add(states[indexes[i]]);
                }
            }
            currentState.Actions.Add(action);
        }

        private bool IsInField(int i, int currentIndex)
        {
            int curX = currentIndex/y;
            int curY = currentIndex%y;
            int iX = i/y;
            int iY = i%y;
            if (iX < 0 || iX >= x) return false;
            if (iY < 0 || iY >= y) return false;
            return (Math.Abs(iY - curY) == 0 && Math.Abs(iX - curX) == 1) ||
                   (Math.Abs(iY - curY) == 1 && Math.Abs(iX - curX) == 0);
            /*if (i < 0 || i >= x * y) return false;
            if (Math.Abs(i - currentIndex) == 1)  return ((i / y) == (currentIndex / y));
            return true;*/
        }

        private void SetTypesAndRewards(List<State> states)
        {
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    int currentIndex = i*y + j;
                    State currentState = new State {Index = i*y+j};
                    Button currentButton = buttons[currentIndex];
                    SetTypeAndReward(currentState, currentButton);
                    states.Add(currentState);
                }
            }
        }

        private void SetTypeAndReward(State currentState, Button currentButton)
        {
            if (currentButton.BackColor == NonTerminalColor)
            {
                currentState.Type = State.StateType.NonTerminal;
                currentState.Reward = r;
            }
            if (currentButton.BackColor == StartColor)
            {
                currentState.Type = State.StateType.Start;
                currentState.Reward = r;
            }
            if (currentButton.BackColor == TerminalColor)
            {
                currentState.Type = State.StateType.Terminal;
                currentState.Reward = double.Parse(currentButton.Text);
            }
            if (currentButton.BackColor == BlockedColor)
            {
                currentState.Type = State.StateType.Blocked;
            }
        }
        #endregion

        private void TextBox1Leave(object sender, EventArgs e)
        {
            double newR;
            if (double.TryParse(rTextbox.Text, NumberStyles.Float,CultureInfo.InvariantCulture,  out newR))
            {
                r = newR;
            }
            else
            {
                rTextbox.Text = "ERROR";
            }
        }

        private void TextBox2Leave(object sender, EventArgs e)
        {
            double newD;
            if (double.TryParse(gammaTextbox.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out newD))
            {
                d = newD;
            }
            else
            {
                gammaTextbox.Text = "ERROR";
            }
        }

        private void CalculateUtilityButtonClick(object sender, EventArgs e)
        {
            CalculateUtility();
        }

        private void ClearButtonClick(object sender, EventArgs e)
        {
            ClearButtons();
        }
    }

    public class Action
    {
        public List<double> Prob = new List<double>();
        public List<State> State = new List<State>();
    }

    public class State
    {
        public enum StateType
        {
            Blocked,
            Terminal,
            Start,
            NonTerminal
        }

        public int Index;
        public StateType Type;
        public double Reward;
        public List<Action> Actions = new List<Action>();

        public override string ToString()
        {
            string res = "State: \nReward: " + Reward + "\n";
            return Actions.Aggregate(res, (current, action) => current + ("Action: \n" + action));

        }

        public static class Randomizer
        {
            private static Random _me;

            public static Random GiveRandomizer()
            {
                return _me ?? (_me = new Random());
            }
        }
    }
}

// ReSharper restore LocalizableElement
