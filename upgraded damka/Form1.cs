using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace upgraded_damka
{
    public partial class Form1 : Form
    {
        Button[,] pic_board;
        int[,] int_board;
        Image[] images = new Image[6] { Properties.Resources.white, Properties.Resources.black, Properties.Resources._3, Properties.Resources._1,Properties.Resources._3_detected,Properties.Resources._1_detected };

        int first_or_second_click = 0;
        int row_place_one = 0;
        int colm_place_one = 0;
        int row_place_two = 0;
        int colm_place_two = 0;
        int attacker = 3;
        int up_down = -1;
        int defender = 4;
        int detected = 5;

        public Form1()
        {
            InitializeComponent();
        }


        private void Button1_Click_1(object sender, EventArgs e)
        {
            pic_board = new Button[8, 8];
            int_board = new int[8, 8]
            {
                {2,0,2,0,2,0,2,0},
                {0,2,0,2,0,2,0,2},
                {2,0,2,0,2,0,2,0},
                {0,1,0,1,0,1,0,1},
                {1,0,1,0,1,0,1,0},
                {0,3,0,3,0,3,0,3},
                {3,0,3,0,3,0,3,0},
                {0,3,0,3,0,3,0,3},
            };
            int row = 0;
            int colm = 0;
            int counter = 0;
            while (row < pic_board.GetLength(0))
            {
                colm = 0;
                while (colm < pic_board.GetLength(1))
                {
                    pic_board[row, colm] = new Button();
                    pic_board[row, colm].Size = new Size(50, 50);
                    pic_board[row, colm].Location = new Point(colm * 50, row * 50);
                    pic_board[row, colm].Tag = counter;
                    pic_board[row, colm].BackgroundImage = images[int_board[row, colm]];
                    pic_board[row, colm].BackgroundImageLayout = ImageLayout.Stretch;
                    this.Controls.Add(pic_board[row, colm]);
                    pic_board[row, colm].Click += Form1_Click;
                    counter++;
                    colm++;
                }
                row++;
            }
        }

        private void Form1_Click(object sender, EventArgs e)
        {
            string s1 = (((Button)(sender)).Tag).ToString();
            int s = int.Parse(s1);
            if (first_or_second_click % 4 == 0 || first_or_second_click % 4 == 1)
            {
                attacker = 3;
                up_down = -1;
                defender = 2;
                detected = 5;
            }
            else
            {
                attacker = 2;
                up_down = 1;
                defender = 3;
                detected = 4;

            }
            if (first_or_second_click % 2 == 0)
            {
                row_place_one = s / int_board.GetLength(1);
                colm_place_one = s % int_board.GetLength(1);
                if (int_board[row_place_one, colm_place_one] == attacker)
                { 
                    pic_board[row_place_one, colm_place_one].BackgroundImage = images[detected];
                    int_board[row_place_one, colm_place_one] = detected;
                    first_or_second_click++;
                }
            }
            else
            {
                row_place_two = s / int_board.GetLength(1);
                colm_place_two = s % int_board.GetLength(1);
                if(int_board[row_place_two,colm_place_two] == detected)
                {
                    pic_board[row_place_one, colm_place_one].BackgroundImage = images[attacker];
                    int_board[row_place_one, colm_place_one] = attacker;
                    first_or_second_click--;
                }
                else
                {
                    if (int_board[row_place_two, colm_place_two] == 1)
                    {
                        if (row_place_two - row_place_one == up_down && Math.Abs(colm_place_two - colm_place_one) == 1)
                        {
                            pic_board[row_place_one, colm_place_one].BackgroundImage = images[1];
                            pic_board[row_place_two, colm_place_two].BackgroundImage = images[attacker];
                            int_board[row_place_one, colm_place_one] = 1;
                            int_board[row_place_two, colm_place_two] = attacker;
                            first_or_second_click++;
                        }
                        else
                        {
                            if (row_place_two - row_place_one == up_down * 2 && Math.Abs(colm_place_two - colm_place_one) == 2)
                            {
                                if (int_board[(row_place_one + row_place_two) / 2, (colm_place_one + colm_place_two) / 2] == defender)
                                {
                                    pic_board[row_place_one, colm_place_one].BackgroundImage = images[1];
                                    pic_board[(row_place_one + row_place_two) / 2, (colm_place_one + colm_place_two) / 2].BackgroundImage = images[1];
                                    pic_board[row_place_two, colm_place_two].BackgroundImage = images[attacker];
                                    int_board[row_place_one, colm_place_one] = 1;
                                    int_board[(row_place_one + row_place_two) / 2, (colm_place_one + colm_place_two) / 2] = 1;
                                    int_board[row_place_two, colm_place_two] = attacker;
                                    first_or_second_click++;
                                }
                            }
                        }
                    }
                }
                button1.Text = first_or_second_click.ToString();
            }
        }
    }
}