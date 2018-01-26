using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tic_Tac_Toe
{
    public partial class Tic_Tac_Toe : Form
    {
        int player;
        int position;
        int difficulty = 2;
        bool againstAI;
        bool boxTest;
        bool meta = false;

        Random random = new Random();
        PictureBox[] boxes = new PictureBox[9];
        PictureBox[] boxes0 = new PictureBox[9];
        PictureBox[] boxes1 = new PictureBox[9];
        PictureBox[] boxes2 = new PictureBox[9];
        PictureBox[] boxes3 = new PictureBox[9];
        PictureBox[] boxes4 = new PictureBox[9];
        PictureBox[] boxes5 = new PictureBox[9];
        PictureBox[] boxes6 = new PictureBox[9];
        PictureBox[] boxes7 = new PictureBox[9];
        PictureBox[] boxes8 = new PictureBox[9];

        PictureBox[] turnBoxes = new PictureBox[9];

        PictureBox[][] mBoxes = new PictureBox[10][];
        int[][] mPositions = new int[10][];
        int location = 9;

        Color turnColor = new Color();

        public Tic_Tac_Toe()
        {
            InitializeComponent();

            createBoxes();

            player = random.Next(1, 3);
            normalMenuItem.Checked = true;
            aiOrPlayer();
            if (player == 1)
            { turnBox2.BackColor = Color.Blue; }
            else { turnBox2.BackColor = Color.Red; }
            if (player == 2 && againstAI == true)
            { aiMove(); }
        } //ends Tic_Tac_Toe constructor

        public void assignBox(int loc,int pos, PictureBox box)
        {
            if(mPositions[loc][pos] != 0)
            { return; }

            if (loc != location)
            { return; }

            int i;
            int n;
            string m;
            position = pos;
            box.Cursor = System.Windows.Forms.Cursors.Default;
            if (meta == true)
            { m = "M"; }
            else { m = ""; }

            if(player == 1)
            {
                box.Load("Resources//" + m + "BlueX.png");
                turnColor = Color.Red;
                mPositions[loc][pos] = 1;
                player++;
            }
            else if (player == 2)
            {
                box.Load("Resources//" + m + "RedO.png");
                turnColor = Color.Blue;
                mPositions[loc][pos] = 2;
                player--;                
            }

            if (meta == true)
            {
                for (i = 0; i < 9; i++)
                {
                    for (n = 0; n < 9; n++)
                    { mBoxes[i][n].Cursor = System.Windows.Forms.Cursors.Default; }
                }
                for (i = 0; i < 9; i++)
                {
                    mBoxes[position][i].Cursor = System.Windows.Forms.Cursors.Hand;

                    if (mPositions[position][i] != 0)
                    { mBoxes[position][i].Cursor = System.Windows.Forms.Cursors.Default; }

                    turnBoxes[i].BackColor = Color.Transparent;
                }
                if (position == 4)
                { 
                    for (i = 0; i < 9; i++)
                    { turnBoxes[i].BackColor = turnColor; }
                }
                else { turnBoxes[position].BackColor = turnColor; }
            }
            else { turnBoxes[2].BackColor = turnColor; }

            checkWin();

            if (againstAI == true)
            {
                if (player == 2)
                {
                    aiMove();
                }
            }
            boxTest = true;
            return;
        } //ends assignBox

        public void checkWin()
        {
            int actPlayer = mPositions[location][position];
            int i;
            bool didWin = false;
            bool metaDidWin = false;

            for (i = 0; i < 3; i++ )
            {
                if (mPositions[location][3 * i] == actPlayer && mPositions[location][3 * i + 1] == actPlayer && mPositions[location][3 * i + 2] == actPlayer)
                { didWin = true; }
            }

            for (i = 0; i < 3; i++)
            {
                if (mPositions[location][i] == actPlayer && mPositions[location][i + 3] == actPlayer && mPositions[location][i + 6] == actPlayer)
                { didWin = true; }
            }

            if (mPositions[location][0] == actPlayer && mPositions[location][4] == actPlayer && mPositions[location][8] == actPlayer)
            { didWin = true; }
            if (mPositions[location][2] == actPlayer && mPositions[location][4] == actPlayer && mPositions[location][6] == actPlayer)
            { didWin = true; }

            if (meta == true && didWin == true && mPositions[9][location] == 0)
            {
                if (actPlayer == 1)
                { boxes[location].Load("Resources//BlueX.png"); }
                if (actPlayer == 2)
                { boxes[location].Load("Resources//RedO.png"); }

                mPositions[9][location] = actPlayer;
                for (i = 0; i < 3; i++)
                {
                    if (mPositions[9][3 * i] == actPlayer && mPositions[9][3 * i + 1] == actPlayer && mPositions[9][3 * i + 2] == actPlayer)
                    { metaDidWin = true; }
                }

                for (i = 0; i < 3; i++)
                {
                    if (mPositions[9][i] == actPlayer && mPositions[9][i + 3] == actPlayer && mPositions[9][i + 6] == actPlayer)
                    { metaDidWin = true; }
                }

                if (mPositions[9][0] == actPlayer && mPositions[9][4] == actPlayer && mPositions[9][8] == actPlayer)
                { metaDidWin = true; }
                if (mPositions[9][2] == actPlayer && mPositions[9][4] == actPlayer && mPositions[9][6] == actPlayer)
                { metaDidWin = true; }
            }

            if (meta == false || metaDidWin == true)
            {
                if (didWin == true)
                { win(actPlayer); }
            }

            if (meta == true)
            { location = position; }

            checkDraw();
        } //ends checkWin

        public void checkDraw()
        {
            int drawCount = 0;
            int mDrawCount = 0;
            int i;
            int n;

            for (i = 0; i < 9; i++)
            {
                for (n = 0; n < 9; n++)
                {
                    if (mPositions[i][n] != 0)
                    { mDrawCount++; }
                }
            }

            for (i = 0; i < 9; i++)
            {
                if (mPositions[location][i] != 0)
                { drawCount++; }
            }
            if (drawCount == 9 && meta == false || mDrawCount == 81)
            {
                DialogResult answer = MessageBox.Show("Do you want to play again?", "Cat's game...", MessageBoxButtons.YesNo);
                string answer2 = answer.ToString();
                if (answer2 == "Yes")
                {
                    restart();
                }
                else
                {
                    Application.Exit();
                }
            }

            if (drawCount == 9 && meta == true)
            {
                location = random.Next(1, 9);
                checkDraw();

                for (i = 0; i < 9; i++)
                {
                    for (n = 0; n < 9; n++)
                    { mBoxes[i][n].Cursor = System.Windows.Forms.Cursors.Default; }
                }
                for (i = 0; i < 9; i++)
                {
                    mBoxes[location][i].Cursor = System.Windows.Forms.Cursors.Hand;
                    if (mPositions[location][i] != 0)
                    { mBoxes[location][i].Cursor = System.Windows.Forms.Cursors.Default; }
                    turnBoxes[i].BackColor = Color.Transparent;
                    if (location == 4)
                    { turnBoxes[i].BackColor = turnColor; }
                }
                turnBoxes[location].BackColor = turnColor;

            }
        } //ends checkDraw

        public void win(int activePlayer)
        {
            string result = "";

            switch (activePlayer)
            {
                case 1:
                    result = "X's win!";
                    break;
                case 2:
                    result = "O's win!";
                    break;
            }

            DialogResult answer = MessageBox.Show("Do you want to play again?", result, MessageBoxButtons.YesNo);
            string answer2 = answer.ToString();
            if (answer2 == "Yes")
            {
                restart();
            }
            else
            {
                Application.Exit();
            }
        } //ends win

        public void restart()
        {
            int i;
            int n;
            for (i = 0; i < 9; i++)
            {
                turnBoxes[i].BackColor = Color.Transparent;

                if (meta == false)
                {
                    boxes[i].BackgroundImage = null;
                    boxes[i].Cursor = System.Windows.Forms.Cursors.Hand;
                    boxes[i].Image = null;
                    mPositions[9][i] = 0;
                }
                if (meta == true)
                {
                    boxes[i].Image = null;
                    boxes[i].Cursor = System.Windows.Forms.Cursors.Default;
                    location = 4;
                    position = 4;

                    for (n = 0; n < 9; n++)
                    {
                        mBoxes[i][n].Image = null;
                        mBoxes[4][n].Cursor = System.Windows.Forms.Cursors.Hand;
                        mPositions[i][n] = 0;
                        mPositions[9][n] = 0;
                    }
                }
            }
            player = random.Next(1, 3);
            aiOrPlayer();
            if (player == 1)
            { turnColor = Color.Blue; }
            else { turnColor = Color.Red; }
            if (meta == true)
            { 
                for (i = 0; i < 9; i++)
                { turnBoxes[i].BackColor = turnColor; }
            }
            else { turnBoxes[2].BackColor = turnColor; }
            if (player == 2 && againstAI == true)
            { aiMove(); }
        } // ends restart

        #region AI methods

        public void aiMove()
        {
            aiResponse(2);
            aiResponse(1);
            aiStrategy();
        } //ends aiMove

        public void aiResponse(int pl)
        {
            if (player != 2)
            { return; }

            int takenBoxes;
            int i;
            int n;
            int missMove;

            missMove = random.Next(1, 11);
            if (difficulty == 4)
            { }
            if (difficulty == 3)
            {
                if (missMove < 2)
                { return; }

            }
            if (difficulty == 2)
            { 
                if (missMove < 5)
                { return; }
            }
            if (difficulty == 1)
            {
                if (missMove < 8)
                { return; }
            }

            for (i = 0; i < 3; i++)
            {
                takenBoxes = 0;
                boxTest = false;

                if (mPositions[location][3*i] == pl) { takenBoxes++; }
                if (mPositions[location][3 * i + 1] == pl) { takenBoxes++; }
                if (mPositions[location][3 * i + 2] == pl) { takenBoxes++; }

                if(takenBoxes == 2)
                {
                    for (n = 0; n < 3; n++)
                    {
                        assignBox(location, 3 * i + n, mBoxes[location][3 * i + n]);
                        if (boxTest == true) { return; }
                    }
                }
            }

            for (i = 0; i < 3; i++)
            {
                takenBoxes = 0;

                if (mPositions[location][i] == pl) { takenBoxes++; }
                if (mPositions[location][i + 3] == pl) { takenBoxes++; }
                if (mPositions[location][i + 6] == pl) { takenBoxes++; }

                if (takenBoxes == 2)
                {
                    for (n = 0; n < 3; n++)
                    {
                        assignBox(location, i + 3 * n, mBoxes[location][i + 3 * n]);
                        if (boxTest == true) { return; }
                    }
                }
            }

            {
                takenBoxes = 0;

                if (mPositions[location][0] == pl) { takenBoxes++; }
                if (mPositions[location][4] == pl) { takenBoxes++; }
                if (mPositions[location][8] == pl) { takenBoxes++; }

                if (takenBoxes == 2)
                {
                    for (i = 0; i < 3; i++)
                    {
                        assignBox(location, 4*i, mBoxes[location][4*i]);
                        if (boxTest == true) { return; }
                    }
                }
            }

            {
                takenBoxes = 0;

                if (mPositions[location][2] == pl) { takenBoxes++; }
                if (mPositions[location][4] == pl) { takenBoxes++; }
                if (mPositions[location][6] == pl) { takenBoxes++; }

                if (takenBoxes == 2)
                {
                    for (i = 0; i < 3; i++)
                    {
                        assignBox(location, 2 + 2 * i, mBoxes[location][2 + 2 * i]);
                        if (boxTest == true) { return; }
                    }
                }
            }

        } //ends aiResponse

        public void aiStrategy()
        {
            if (player != 2)
            { return; }

            int choice;
            int chance;

            chance = random.Next(0, 10 * difficulty);

            if (chance > 3 || difficulty == 4)
            {
                if (mPositions[location][4] == 0)
                { assignBox(location, 4, mBoxes[location][4]); return; }
            }

            if (chance > 6 || difficulty == 4)
            {
                while (mPositions[location][0] == 0 || mPositions[location][2] == 0 || mPositions[location][6] == 0 || mPositions[location][8] == 0)
                {
                    choice = random.Next(0, 5);
                    assignBox(location, choice * 2, mBoxes[location][choice * 2]);
                    if (player != 2)
                    { return; }
                }
            }

            if (chance > 5 || difficulty == 4)
            {
                while (mPositions[location][1] == 0 || mPositions[location][3] == 0 || mPositions[location][5] == 0 || mPositions[location][7] == 0)
                {
                    choice = random.Next(0, 4);
                    assignBox(location, choice * 2 + 1, mBoxes[location][choice * 2 + 1]);
                    if (player != 2)
                    { return; }
                }
            }

            while (player == 2 || difficulty == 4)
            {
                choice = random.Next(0, 9);
                assignBox(location, choice, mBoxes[location][choice]);
                int i;
                int count = 0;
                for (i = 0; i < 9; i++)
                {
                    if (mPositions[location][i] != 0)
                    { count++; }
                    if (count == 9)
                    { return; }
                }
            }
        } //ends aiStrategy

        #endregion

        public void aiOrPlayer()
        {
            DialogResult answer = MessageBox.Show("Computer = Yes \r\nOther player = No \r\n\r\n Note: Meta is intended for PvP\r\n\r\n(Right-click in-game for settings)", "Against the computer?", MessageBoxButtons.YesNo);
            string answer2 = answer.ToString();
            if (answer2 == "Yes")
            { againstAI = true; }
            else
            { againstAI = false; }
        } //ends aiOrPlayer

        public void createBoxes()
        {
            int i;

            boxes[0] = topLBox;
            boxes[1] = topMBox;
            boxes[2] = topRBox;
            boxes[3] = leftBox;
            boxes[4] = middleBox;
            boxes[5] = rightBox;
            boxes[6] = botLBox;
            boxes[7] = botMBox;
            boxes[8] = botRBox;

            boxes0[0] = metaTLBox0;
            boxes0[1] = metaTLBox1;
            boxes0[2] = metaTLBox2;
            boxes0[3] = metaTLBox3;
            boxes0[4] = metaTLBox4;
            boxes0[5] = metaTLBox5;
            boxes0[6] = metaTLBox6;
            boxes0[7] = metaTLBox7;
            boxes0[8] = metaTLBox8;

            boxes1[0] = metaTMBox0;
            boxes1[1] = metaTMBox1;
            boxes1[2] = metaTMBox2;
            boxes1[3] = metaTMBox3;
            boxes1[4] = metaTMBox4;
            boxes1[5] = metaTMBox5;
            boxes1[6] = metaTMBox6;
            boxes1[7] = metaTMBox7;
            boxes1[8] = metaTMBox8;

            boxes2[0] = metaTRBox0;
            boxes2[1] = metaTRBox1;
            boxes2[2] = metaTRBox2;
            boxes2[3] = metaTRBox3;
            boxes2[4] = metaTRBox4;
            boxes2[5] = metaTRBox5;
            boxes2[6] = metaTRBox6;
            boxes2[7] = metaTRBox7;
            boxes2[8] = metaTRBox8;

            boxes3[0] = metaLBox0;
            boxes3[1] = metaLBox1;
            boxes3[2] = metaLBox2;
            boxes3[3] = metaLBox3;
            boxes3[4] = metaLBox4;
            boxes3[5] = metaLBox5;
            boxes3[6] = metaLBox6;
            boxes3[7] = metaLBox7;
            boxes3[8] = metaLBox8;

            boxes4[0] = metaMBox0;
            boxes4[1] = metaMBox1;
            boxes4[2] = metaMBox2;
            boxes4[3] = metaMBox3;
            boxes4[4] = metaMBox4;
            boxes4[5] = metaMBox5;
            boxes4[6] = metaMBox6;
            boxes4[7] = metaMBox7;
            boxes4[8] = metaMBox8;

            boxes5[0] = metaRBox0;
            boxes5[1] = metaRBox1;
            boxes5[2] = metaRBox2;
            boxes5[3] = metaRBox3;
            boxes5[4] = metaRBox4;
            boxes5[5] = metaRBox5;
            boxes5[6] = metaRBox6;
            boxes5[7] = metaRBox7;
            boxes5[8] = metaRBox8;

            boxes6[0] = metaBLBox0;
            boxes6[1] = metaBLBox1;
            boxes6[2] = metaBLBox2;
            boxes6[3] = metaBLBox3;
            boxes6[4] = metaBLBox4;
            boxes6[5] = metaBLBox5;
            boxes6[6] = metaBLBox6;
            boxes6[7] = metaBLBox7;
            boxes6[8] = metaBLBox8;

            boxes7[0] = metaBMBox0;
            boxes7[1] = metaBMBox1;
            boxes7[2] = metaBMBox2;
            boxes7[3] = metaBMBox3;
            boxes7[4] = metaBMBox4;
            boxes7[5] = metaBMBox5;
            boxes7[6] = metaBMBox6;
            boxes7[7] = metaBMBox7;
            boxes7[8] = metaBMBox8;

            boxes8[0] = metaBRBox0;
            boxes8[1] = metaBRBox1;
            boxes8[2] = metaBRBox2;
            boxes8[3] = metaBRBox3;
            boxes8[4] = metaBRBox4;
            boxes8[5] = metaBRBox5;
            boxes8[6] = metaBRBox6;
            boxes8[7] = metaBRBox7;
            boxes8[8] = metaBRBox8;

            mBoxes[0] = boxes0;
            mBoxes[1] = boxes1;
            mBoxes[2] = boxes2;
            mBoxes[3] = boxes3;
            mBoxes[4] = boxes4;
            mBoxes[5] = boxes5;
            mBoxes[6] = boxes6;
            mBoxes[7] = boxes7;
            mBoxes[8] = boxes8;
            mBoxes[9] = boxes;

            for (i = 0; i < 10; i++)
            { mPositions[i] = new int[9]; }

            turnBoxes[0] = turnBox0;
            turnBoxes[1] = turnBox1;
            turnBoxes[2] = turnBox2;
            turnBoxes[3] = turnBox3;
            turnBoxes[4] = turnBox2;
            turnBoxes[5] = turnBox5;
            turnBoxes[6] = turnBox6;
            turnBoxes[7] = turnBox7;
            turnBoxes[8] = turnBox8;
        } //ends createBoxes

        #region Box Clicks

        private void topLBox_Click(object sender, EventArgs e)
        {
            if (meta == false)
                assignBox(9, 0, boxes[0]);
        }

        private void topMBox_Click(object sender, EventArgs e)
        {
            if (meta == false)
                assignBox(9, 1, boxes[1]);
        }

        private void topRBox_Click(object sender, EventArgs e)
        {
            if (meta == false)
                assignBox(9, 2, boxes[2]);
        }

        private void leftBox_Click(object sender, EventArgs e)
        {
            if (meta == false)
                assignBox(9, 3, boxes[3]);
        }

        private void middleBox_Click(object sender, EventArgs e)
        {
            if (meta == false)
                assignBox(9, 4, boxes[4]);
        }

        private void rightBox_Click(object sender, EventArgs e)
        {
            if (meta == false)
                assignBox(9, 5, boxes[5]);
        }

        private void botLBox_Click(object sender, EventArgs e)
        {
            if (meta == false)
                assignBox(9, 6, boxes[6]);
        }

        private void botMBox_Click(object sender, EventArgs e)
        {
            if (meta == false)
                assignBox(9, 7, boxes[7]);
        }

        private void botRBox_Click(object sender, EventArgs e)
        {
            if (meta == false)
                assignBox(9, 8, boxes[8]);
        }

        #endregion

        #region Meta Box Clicks

        #region Top Left Box

        private void metaTLBox0_Click(object sender, EventArgs e)
        {
            if (meta == true)
                assignBox(0, 0, mBoxes[0][0]);
        }

        private void metaTLBox1_Click(object sender, EventArgs e)
        {
            if (meta == true)
                assignBox(0, 1, mBoxes[0][1]);
        }

        private void metaTLBox2_Click(object sender, EventArgs e)
        {
            if (meta == true)
                assignBox(0, 2, mBoxes[0][2]);
        }

        private void metaTLBox3_Click(object sender, EventArgs e)
        {
            if (meta == true)
                assignBox(0, 3, mBoxes[0][3]);
        }

        private void metaTLBox4_Click(object sender, EventArgs e)
        {
            if (meta == true)
                assignBox(0, 4, mBoxes[0][4]);
        }

        private void metaTLBox5_Click(object sender, EventArgs e)
        {
            if (meta == true)
                assignBox(0, 5, mBoxes[0][5]);
        }

        private void metaTLBox6_Click(object sender, EventArgs e)
        {
            if (meta == true)
                assignBox(0, 6, mBoxes[0][6]);
        }

        private void metaTLBox7_Click(object sender, EventArgs e)
        {
            if (meta == true)
                assignBox(0, 7, mBoxes[0][7]);
        }

        private void metaTLBox8_Click(object sender, EventArgs e)
        {
            if (meta == true)
                assignBox(0, 8, mBoxes[0][8]);
        }


        #endregion

        #region Top Middle Box

        private void metaTMBox0_Click(object sender, EventArgs e)
        {
            if (meta == true)
                assignBox(1, 0, mBoxes[1][0]);
        }

        private void metaTMBox1_Click(object sender, EventArgs e)
        {
            if (meta == true)
                assignBox(1, 1, mBoxes[1][1]);
        }

        private void metaTMBox2_Click(object sender, EventArgs e)
        {
            if (meta == true)
                assignBox(1, 2, mBoxes[1][2]);
        }

        private void metaTMBox3_Click(object sender, EventArgs e)
        {
            if (meta == true)
                assignBox(1, 3, mBoxes[1][3]);
        }

        private void metaTMBox4_Click(object sender, EventArgs e)
        {
            if (meta == true)
                assignBox(1, 4, mBoxes[1][4]);
        }

        private void metaTMBox5_Click(object sender, EventArgs e)
        {
            if (meta == true)
                assignBox(1, 5, mBoxes[1][5]);
        }

        private void metaTMBox6_Click(object sender, EventArgs e)
        {
            if (meta == true)
                assignBox(1, 6, mBoxes[1][6]);
        }

        private void metaTMBox7_Click(object sender, EventArgs e)
        {
            if (meta == true)
                assignBox(1, 7, mBoxes[1][7]);
        }

        private void metaTMBox8_Click(object sender, EventArgs e)
        {
            if (meta == true)
                assignBox(1, 8, mBoxes[1][8]);
        }


        #endregion

        #region Top Right Box

        private void metaTRBox0_Click(object sender, EventArgs e)
        {
            if (meta == true)
                assignBox(2, 0, mBoxes[2][0]);
        }

        private void metaTRBox1_Click(object sender, EventArgs e)
        {
            if (meta == true)
                assignBox(2, 1, mBoxes[2][1]);
        }

        private void metaTRBox2_Click(object sender, EventArgs e)
        {
            if (meta == true)
                assignBox(2, 2, mBoxes[2][2]);
        }

        private void metaTRBox3_Click(object sender, EventArgs e)
        {
            if (meta == true)
                assignBox(2, 3, mBoxes[2][3]);
        }

        private void metaTRBox4_Click(object sender, EventArgs e)
        {
            if (meta == true)
                assignBox(2, 4, mBoxes[2][4]);
        }

        private void metaTRBox5_Click(object sender, EventArgs e)
        {
            if (meta == true)
                assignBox(2, 5, mBoxes[2][5]);
        }

        private void metaTRBox6_Click(object sender, EventArgs e)
        {
            if (meta == true)
                assignBox(2, 6, mBoxes[2][6]);
        }

        private void metaTRBox7_Click(object sender, EventArgs e)
        {
            if (meta == true)
                assignBox(2, 7, mBoxes[2][7]);
        }

        private void metaTRBox8_Click(object sender, EventArgs e)
        {
            if (meta == true)
                assignBox(2, 8, mBoxes[2][8]);
        }

        #endregion

        #region Left Box

        private void metaLBox0_Click(object sender, EventArgs e)
        {
            if (meta == true)
                assignBox(3, 0, mBoxes[3][0]);
        }

        private void metaLBox1_Click(object sender, EventArgs e)
        {
            if (meta == true)
                assignBox(3, 1, mBoxes[3][1]);
        }

        private void metaLBox2_Click(object sender, EventArgs e)
        {
            if (meta == true)
                assignBox(3, 2, mBoxes[3][2]);
        }

        private void metaLBox3_Click(object sender, EventArgs e)
        {
            if (meta == true)
                assignBox(3, 3, mBoxes[3][3]);
        }

        private void metaLBox4_Click(object sender, EventArgs e)
        {
            if (meta == true)
                assignBox(3, 4, mBoxes[3][4]);
        }

        private void metaLBox5_Click(object sender, EventArgs e)
        {
            if (meta == true)
                assignBox(3, 5, mBoxes[3][5]);
        }

        private void metaLBox6_Click(object sender, EventArgs e)
        {
            if (meta == true)
                assignBox(3, 6, mBoxes[3][6]);
        }

        private void metaLBox7_Click(object sender, EventArgs e)
        {
            if (meta == true)
                assignBox(3, 7, mBoxes[3][7]);
        }

        private void metaLBox8_Click(object sender, EventArgs e)
        {
            if (meta == true)
                assignBox(3, 8, mBoxes[3][8]);
        }

        #endregion

        #region Middle Box

        private void metaMBox0_Click(object sender, EventArgs e)
        {
            if (meta == true)
                assignBox(4, 0, mBoxes[4][0]);
        }

        private void metaMBox1_Click(object sender, EventArgs e)
        {
            if (meta == true)
                assignBox(4, 1, mBoxes[4][1]);
        }

        private void metaMBox2_Click(object sender, EventArgs e)
        {
            if (meta == true)
                assignBox(4, 2, mBoxes[4][2]);
        }

        private void metaMBox3_Click(object sender, EventArgs e)
        {
            if (meta == true)
                assignBox(4, 3, mBoxes[4][3]);
        }

        private void metaMBox4_Click(object sender, EventArgs e)
        {
            if (meta == true)
                assignBox(4, 4, mBoxes[4][4]);
        }

        private void metaMBox5_Click(object sender, EventArgs e)
        {
            if (meta == true)
                assignBox(4, 5, mBoxes[4][5]);
        }

        private void metaMBox6_Click(object sender, EventArgs e)
        {
            if (meta == true)
                assignBox(4, 6, mBoxes[4][6]);
        }

        private void metaMBox7_Click(object sender, EventArgs e)
        {
            if (meta == true)
                assignBox(4, 7, mBoxes[4][7]);
        }

        private void metaMBox8_Click(object sender, EventArgs e)
        {
            if (meta == true)
                assignBox(4, 8, mBoxes[4][8]);
        }

        #endregion

        #region Right Box

        private void metaRBox0_Click(object sender, EventArgs e)
        {
            if (meta == true)
                assignBox(5, 0, mBoxes[5][0]);
        }

        private void metaRBox1_Click(object sender, EventArgs e)
        {
            if (meta == true)
                assignBox(5, 1, mBoxes[5][1]);
        }

        private void metaRBox2_Click(object sender, EventArgs e)
        {
            if (meta == true)
                assignBox(5, 2, mBoxes[5][2]);
        }

        private void metaRBox3_Click(object sender, EventArgs e)
        {
            if (meta == true)
                assignBox(5, 3, mBoxes[5][3]);
        }

        private void metaRBox4_Click(object sender, EventArgs e)
        {
            if (meta == true)
                assignBox(5, 4, mBoxes[5][4]);
        }

        private void metaRBox5_Click(object sender, EventArgs e)
        {
            if (meta == true)
                assignBox(5, 5, mBoxes[5][5]);
        }

        private void metaRBox6_Click(object sender, EventArgs e)
        {
            if (meta == true)
                assignBox(5, 6, mBoxes[5][6]);
        }

        private void metaRBox7_Click(object sender, EventArgs e)
        {
            if (meta == true)
                assignBox(5, 7, mBoxes[5][7]);
        }

        private void metaRBox8_Click(object sender, EventArgs e)
        {
            if (meta == true)
                assignBox(5, 8, mBoxes[5][8]);
        }

        #endregion

        #region Bottom Left Box

        private void metaBLBox0_Click(object sender, EventArgs e)
        {
            if (meta == true)
                assignBox(6, 0, mBoxes[6][0]);
        }

        private void metaBLBox1_Click(object sender, EventArgs e)
        {
            if (meta == true)
                assignBox(6, 1, mBoxes[6][1]);
        }

        private void metaBLBox2_Click(object sender, EventArgs e)
        {
            if (meta == true)
                assignBox(6, 2, mBoxes[6][2]);
        }

        private void metaBLBox3_Click(object sender, EventArgs e)
        {
            if (meta == true)
                assignBox(6, 3, mBoxes[6][3]);
        }

        private void metaBLBox4_Click(object sender, EventArgs e)
        {
            if (meta == true)
                assignBox(6, 4, mBoxes[6][4]);
        }

        private void metaBLBox5_Click(object sender, EventArgs e)
        {
            if (meta == true)
                assignBox(6, 5, mBoxes[6][5]);
        }

        private void metaBLBox6_Click(object sender, EventArgs e)
        {
            if (meta == true)
                assignBox(6, 6, mBoxes[6][6]);
        }

        private void metaBLBox7_Click(object sender, EventArgs e)
        {
            if (meta == true)
                assignBox(6, 7, mBoxes[6][7]);
        }

        private void metaBLBox8_Click(object sender, EventArgs e)
        {
            if (meta == true)
                assignBox(6, 8, mBoxes[6][8]);
        }

        #endregion

        #region Bottom Middle Box

        private void metaBMBox0_Click(object sender, EventArgs e)
        {
            if (meta == true)
                assignBox(7, 0, mBoxes[7][0]);
        }

        private void metaBMBox1_Click(object sender, EventArgs e)
        {
            if (meta == true)
                assignBox(7, 1, mBoxes[7][1]);
        }

        private void metaBMBox2_Click(object sender, EventArgs e)
        {
            if (meta == true)
                assignBox(7, 2, mBoxes[7][2]);
        }

        private void metaBMBox3_Click(object sender, EventArgs e)
        {
            if (meta == true)
                assignBox(7, 3, mBoxes[7][3]);
        }

        private void metaBMBox4_Click(object sender, EventArgs e)
        {
            if (meta == true)
                assignBox(7, 4, mBoxes[7][4]);
        }

        private void metaBMBox5_Click(object sender, EventArgs e)
        {
            if (meta == true)
                assignBox(7, 5, mBoxes[7][5]);
        }

        private void metaBMBox6_Click(object sender, EventArgs e)
        {
            if (meta == true)
                assignBox(7, 6, mBoxes[7][6]);
        }

        private void metaBMBox7_Click(object sender, EventArgs e)
        {
            if (meta == true)
                assignBox(7, 7, mBoxes[7][7]);
        }

        private void metaBMBox8_Click(object sender, EventArgs e)
        {
            if (meta == true)
                assignBox(7, 8, mBoxes[7][8]);
        }

        #endregion

        #region Bottom Right Box

        private void metaBRBox0_Click(object sender, EventArgs e)
        {
            if (meta == true)
                assignBox(8, 0, mBoxes[8][0]);
        }

        private void metaBRBox1_Click(object sender, EventArgs e)
        {
            if (meta == true)
                assignBox(8, 1, mBoxes[8][1]);
        }

        private void metaBRBox2_Click(object sender, EventArgs e)
        {
            if (meta == true)
                assignBox(8, 2, mBoxes[8][2]);
        }

        private void metaBRBox3_Click(object sender, EventArgs e)
        {
            if (meta == true)
                assignBox(8, 3, mBoxes[8][3]);
        }

        private void metaBRBox4_Click(object sender, EventArgs e)
        {
            if (meta == true)
                assignBox(8, 4, mBoxes[8][4]);
        }

        private void metaBRBox5_Click(object sender, EventArgs e)
        {
            if (meta == true)
                assignBox(8, 5, mBoxes[8][5]);
        }

        private void metaBRBox6_Click(object sender, EventArgs e)
        {
            if (meta == true)
                assignBox(8, 6, mBoxes[8][6]);
        }

        private void metaBRBox7_Click(object sender, EventArgs e)
        {
            if (meta == true)
                assignBox(8, 7, mBoxes[8][7]);
        }

        private void metaBRBox8_Click(object sender, EventArgs e)
        {
            if (meta == true)
                assignBox(8, 8, mBoxes[8][8]);
        }

        #endregion

        #endregion

        #region Settings Strip

        private void easyMenuItem_Click(object sender, EventArgs e)
        {
            restart();
            difficulty = 1;
            easyMenuItem.Checked = true;
            normalMenuItem.Checked = false;
            hardMenuItem.Checked = false;
            impossibleMenuItem.Checked = false;
        }

        private void normalMenuItem_Click(object sender, EventArgs e)
        {
            restart();
            difficulty = 2;
            easyMenuItem.Checked = false;
            normalMenuItem.Checked = true;
            hardMenuItem.Checked = false;
            impossibleMenuItem.Checked = false;
        }

        private void hardMenuItem_Click(object sender, EventArgs e)
        {
            restart();
            difficulty = 3;
            easyMenuItem.Checked = false;
            normalMenuItem.Checked = false;
            hardMenuItem.Checked = true;
            impossibleMenuItem.Checked = false;
        }

        private void impossibleMenuItem_Click(object sender, EventArgs e)
        {
            restart();
            difficulty = 4;
            easyMenuItem.Checked = false;
            normalMenuItem.Checked = false;
            hardMenuItem.Checked = false;
            impossibleMenuItem.Checked = true;
        }

        private void metaGameMenuItem_Click(object sender, EventArgs e)
        {
            meta = !meta;
            metaGameMenuItem.Checked = !metaGameMenuItem.Checked;
            int i;
            int n;

            if (meta == true)
            {
                for (i = 0; i < 9; i++)
                { 
                    boxes[i].BackgroundImage = Image.FromFile("Resources//MBox.png");
                    location = 4;
                    for (n = 0; n < 9; n++)
                    {
                        boxes[i].Image = null;
                        mBoxes[i][n].BringToFront();
                    }
                }
                restart();
                MessageBox.Show("Meta Tic-Tac-Toe is basically Tic-Tac-Toe inside Tic-Tac-Toe.  Each selection within a small Tic-Tac-Toe game will determine which box the next move happens in. In order to win, three small game boxes in sequence must be won.", "How to play Meta:");
            }
            if (meta == false)
            {
                for (i = 0; i < 9; i++)
                { 
                    boxes[i].BackgroundImage = null;
                    location = 9;
                    for (n = 0; n < 9; n++)
                    {
                        mBoxes[i][n].Image = null;
                        mBoxes[i][n].SendToBack();
                    }
                }
                restart();
            }
        }

        #endregion
    }
}
