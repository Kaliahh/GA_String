using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;

namespace GA_String
{
    class GAForm : Form
    {
        Button btnRunGA;

        TextBox txtTarget;
        TextBox txtMutationRate;
        TextBox txtPopSize;
        TextBox txtSlowDown;
        TextBox txtOutput;

        Label lblTarget;
        Label lblMutationRate;
        Label lblPopSize;
        Label lblSlowDown;
        Label lblOutput;

        private int spacing = 25;

        public GAForm()
        {
            int width = 700;
            int height = 230;
            this.Size = new Size(width, height);

            MakeLabels();
            MakeTextboxes();
            MakeButtons();
            AddEvents();
            AddControls();
        }

        void MakeLabels()
        {
            int lblWidth = 150;
            int lblHeight = 20;

            lblTarget = new Label
            {
                Size = new Size(lblWidth, lblHeight),
                Location = new Point(10, 10),
                Text = "Target: ",
                Font = new Font("Consolas", 11)
            };

            lblMutationRate = new Label
            {
                Size = new Size(lblWidth, lblHeight),
                Location = new Point(lblTarget.Location.X, lblTarget.Location.Y + spacing),
                Text = "Mutation rate: ",
                Font = new Font("Consolas", 11)
            };

            lblPopSize = new Label
            {
                Size = new Size(lblWidth, lblHeight),
                Location = new Point(lblMutationRate.Location.X, lblMutationRate.Location.Y + spacing),
                Text = "Population size: ",
                Font = new Font("Consolas", 11)
            };

            lblSlowDown = new Label
            {
                Size = new Size(lblWidth, lblHeight),
                Location = new Point(lblPopSize.Location.X, lblPopSize.Location.Y + spacing),
                Text = "Slow down rate: ",
                Font = new Font("Consolas", 11)
            };

            lblOutput = new Label
            {
                Size = new Size(lblWidth, lblHeight),
                Location = new Point(lblSlowDown.Location.X, lblSlowDown.Location.Y + spacing),
                Text = "Output: ",
                Font = new Font("Consolas", 11),
                AutoSize = true
            };

            
        }

        void MakeTextboxes()
        {
            int txtWidth = 100;
            int txtHeight = 50;

            txtTarget = new TextBox
            {
                Size = new Size(txtWidth + 400, txtHeight),
                Location = new Point(150, 10),
                Text = "This is the string that will be searched for",
                Font = new Font("Consolas", 11),
                MaxLength = 56
            };

            txtMutationRate = new TextBox
            {
                Size = new Size(txtWidth / 2, txtHeight),
                Location = new Point(txtTarget.Location.X, txtTarget.Location.Y + spacing),
                Text = "1",
                Font = new Font("Consolas", 11)
            };

            txtPopSize = new TextBox
            {
                Size = new Size(txtWidth / 2, txtHeight),
                Location = new Point(txtMutationRate.Location.X, txtMutationRate.Location.Y + spacing),
                Text = "2000",
                Font = new Font("Consolas", 11)
            };

            txtSlowDown = new TextBox
            {
                Size = new Size(txtWidth / 2, txtHeight),
                Location = new Point(txtPopSize.Location.X, txtPopSize.Location.Y + spacing),
                Text = "0",
                Font = new Font("Consolas", 11)
            };

            txtOutput = new TextBox
            {
                Size = new Size(txtWidth + 400, txtHeight),
                Location = new Point(txtSlowDown.Location.X, txtSlowDown.Location.Y + spacing),
                Text = "",
                Font = new Font("Consolas", 11),
                Enabled = false
            };
        }

        void MakeButtons()
        {
            btnRunGA = new Button
            {
                Size = new Size(70, 30),
                Location = new Point(10, 150),
                Text = "Run",
                Font = new Font("Consolas", 11)
            };
        }

        void AddEvents()
        {
            btnRunGA.Click += BtnRunGA_Click;
        }

        void AddControls()
        {
            Controls.Add(txtTarget);
            Controls.Add(txtMutationRate);
            Controls.Add(txtPopSize);
            Controls.Add(txtSlowDown);
            Controls.Add(txtOutput);

            Controls.Add(lblTarget);
            Controls.Add(lblMutationRate);
            Controls.Add(lblPopSize);
            Controls.Add(lblSlowDown);
            Controls.Add(lblOutput);

            Controls.Add(btnRunGA);
        }

        private void BtnRunGA_Click(object sender, EventArgs e)
        {
            string target;
            int mutationRate;
            int popSize;

            Population population;

            try
            {
                target = txtTarget.Text;
                mutationRate = int.Parse(txtMutationRate.Text);
                popSize = int.Parse(txtPopSize.Text);

                this.Size = new Size(700, 230);

                population = new Population(target, mutationRate, popSize);

                Run(population);
            }

            catch (FormatException a)
            {
                MessageBox.Show(a.Message);
            }

            catch (OverflowException a)
            {
                MessageBox.Show(a.Message);
            }
        }

        void Run(Population population)
        {
            // var timer = Stopwatch.StartNew();

            while (population.finished == false) // Bliver ved indtil fitness-værdien når 100
            {
                population.DoMagic(); // Gør alle de der GA ting
                Thread.Sleep(int.Parse(txtSlowDown.Text));

                txtOutput.Text = new string(population.best.genes) + " | " + population.generation;
                txtOutput.Refresh();
            }

            // timer.Stop();
        }
    }
}
