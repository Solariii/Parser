using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Security;
using System.Drawing.Imaging;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Drawing.Drawing2D;
using System.Drawing.Text;
namespace Parser
{
    public partial class Parser : Form
    {
        List<Panel> listPanel = new List<Panel>();
        int index;
        public Parser()
        {
            InitializeComponent();
        }



        interface IDrawable
        {
            // Return the object's needed size.
            SizeF GetSize(Graphics gr, Font font);

            // Draw the object centered at (x, y).
            void Draw(float x, float y, Graphics gr, Pen pen,
                Brush bg_brush, Brush text_brush, Font font, int type);
        }

        class TreeNode<T> where T : IDrawable
        {
            // The data.
            public T Data;
            public int type;//0 circle 1 rectangle

            // Child nodes in the tree.
            public List<TreeNode<T>> Children = new List<TreeNode<T>>();
            public List<TreeNode<T>> Left_Neighbour = new List<TreeNode<T>>();
            public List<TreeNode<T>> Right_Neighbour = new List<TreeNode<T>>();

            // Space to skip horizontally between siblings
            // and vertically between generations.
            private const float Hoffset = 50;
            private const float Voffset = 100;

            // The node's center after arranging.
            private PointF Center;

            // Drawing properties.
            public Font MyFont = null;
            public Pen MyPen = Pens.Black;
            public Brush FontBrush = Brushes.Green;
            public Brush BgBrush = Brushes.White;

            // Constructor.
            public TreeNode()
            {

            }
            public TreeNode(T new_data)
                : this(new_data, new Font("Times New Roman", 16))
            {
                Data = new_data;
            }
            public TreeNode(T new_data, int type1)
                : this(new_data, new Font("Times New Roman", 16))
            {
                Data = new_data;
                type = type1;
            }
            public TreeNode(T new_data, Font fg_font)
            {
                Data = new_data;
                MyFont = fg_font;
            }

            // Add a TreeNode to out Children list.
            public void AddChild(TreeNode<T> child)
            {
                Children.Add(child);
            }
            public void AddLeftNeighbour(TreeNode<T> child)
            {
                Left_Neighbour.Add(child);
            }
            public void AddRightNeighbour(TreeNode<T> child)
            {
                Right_Neighbour.Add(child);
            }

            // Arrange the node and its children in the allowed area.
            // Set xmin to indicate the right edge of our subtree.
            // Set ymin to indicate the bottom edge of our subtree.
            public void Arrange(Graphics gr, ref float xmin, ref float ymin)
            {
                xmin = xmin + 50;

                // See how big this node is.
                SizeF my_size = Data.GetSize(gr, MyFont);

                // Recursively arrange our children,
                // allowing room for this node.
                float x = xmin;
                float biggest_ymin = ymin + my_size.Height;
                float subtree_ymin = ymin + my_size.Height + Voffset;

                foreach (TreeNode<T> child in Left_Neighbour)
                {
                    // Arrange this child's subtree.
                    float child_ymin = ymin;
                    float child_xmin = xmin - 300;
                    child.Arrange(gr, ref child_xmin, ref child_ymin);

                    // See if this increases the biggest ymin value.
                    //   if (biggest_ymin < child_ymin) biggest_ymin = child_ymin;

                    // Allow room before the next sibling.
                    x += Hoffset;
                }
                foreach (TreeNode<T> child in Children)
                {
                    if (Children.Count == 3)
                        if (child == Children[2])
                        {
                            x = x + 1500;
                        }
                    // Arrange this child's subtree.
                    float child_ymin = subtree_ymin;
                    child.Arrange(gr, ref x, ref child_ymin);
                    // See if this increases the biggest ymin value.
                    if (biggest_ymin < child_ymin) biggest_ymin = child_ymin;
                    // Allow room before the next sibling.
                    x += Hoffset + 100;
                    if (Globals.picture_box_max_x < child.Center.X)
                        Globals.picture_box_max_x = (int)(child.Center.X);
                    if (Globals.picture_box_max_y < child.Center.Y)
                        Globals.picture_box_max_y = (int)(child.Center.Y);
                }
                foreach (TreeNode<T> child in Right_Neighbour)
                {
                    // Arrange this child's subtree.
                    float child_ymin = ymin;
                    float child_xmin = x + 100 * (4 - child.Children.Count());
                    x = x + 100 * (5 - child.Children.Count());

                    child.Arrange(gr, ref child_xmin, ref child_ymin);

                    // See if this increases the biggest ymin value.
                    //  if (biggest_ymin < child_ymin) biggest_ymin = child_ymin;

                    // Allow room before the next sibling.

                    x += Hoffset + 200;
                    if (Globals.picture_box_max_x < child.Center.X)
                        Globals.picture_box_max_x = (int)(child.Center.X);
                    if (Globals.picture_box_max_y < child.Center.Y)
                        Globals.picture_box_max_y = (int)(child.Center.Y);
                }
                // Remove the spacing after the last child.
                if (Children.Count > 0) x -= Hoffset;

                // See if this node is wider than the subtree under it.
                float subtree_width = x - xmin;
                if (my_size.Width > subtree_width)
                {
                    // Center the subtree under this node.
                    // Make the children rearrange themselves
                    // moved to center their subtrees.
                    x = xmin + (my_size.Width - subtree_width) / 2;
                    foreach (TreeNode<T> child in Children)
                    {
                        // Arrange this child's subtree.
                        child.Arrange(gr, ref x, ref subtree_ymin);

                        // Allow room before the next sibling.
                        x += Hoffset;
                    }
                    // The subtree's width is this node's width.
                    subtree_width = my_size.Width;
                }

                // Set this node's center position.
                Center = new PointF(
                    xmin + subtree_width / 2,
                    ymin + my_size.Height / 2);

                // Increase xmin to allow room for
                // the subtree before returning.
                xmin += subtree_width;

                // Set the return value for ymin.
                ymin = biggest_ymin;
            }

            // Draw the subtree rooted at this node
            // with the given upper left corner.
            public void DrawTree(Graphics gr, ref float x, float y)
            {
                // Arrange the tree.
                Arrange(gr, ref x, ref y);

                // Draw the tree.
                DrawTree(gr);
            }

            // Draw the subtree rooted at this node.
            public void DrawTree(Graphics gr)
            {
                // Draw the links.
                DrawSubtreeLinks(gr);

                // Draw the nodes.
                DrawSubtreeNodes(gr);
            }

            // Draw the links for the subtree rooted at this node.
            private void DrawSubtreeLinks(Graphics gr)
            {
                foreach (TreeNode<T> child in Children)
                {
                    // Draw the link between this node this child.
                    gr.DrawLine(MyPen, Center, child.Center);
                    // Recursively make the child draw its subtree nodes.
                    child.DrawSubtreeLinks(gr);
                }
                foreach (TreeNode<T> child in Left_Neighbour)
                {
                    // Draw the link between this node this child.
                    gr.DrawLine(MyPen, Center, child.Center);
                    // Recursively make the child draw its subtree nodes.
                    child.DrawSubtreeLinks(gr);
                }
                foreach (TreeNode<T> child in Right_Neighbour)
                {
                    // Draw the link between this node this child.
                    gr.DrawLine(MyPen, Center, child.Center);
                    // Recursively make the child draw its subtree nodes.
                    child.DrawSubtreeLinks(gr);
                }
            }

            // Draw the nodes for the subtree rooted at this node.
            private void DrawSubtreeNodes(Graphics gr)
            {
                // Draw this node.
                Data.Draw(Center.X, Center.Y, gr, MyPen, BgBrush, FontBrush, MyFont, type);

                // Recursively make the child draw its subtree nodes.
                foreach (TreeNode<T> child in Children)
                {
                    child.DrawSubtreeNodes(gr);
                }
                foreach (TreeNode<T> child in Left_Neighbour)
                {



                    child.DrawSubtreeNodes(gr);

                    /*
                    
                    // child.DrawSubtreeNodes(gr);
                    child.Data.Draw(Center.X - 100, Center.Y, gr, MyPen, BgBrush, FontBrush, MyFont);
                  //  child.DrawSubtreeNodes(gr);*/

                }
                foreach (TreeNode<T> child in Right_Neighbour)
                {


                    child.DrawSubtreeNodes(gr);


                    /*
                   // child.DrawSubtreeNodes(gr);
                    child.Data.Draw(Center.X + 100, Center.Y, gr, MyPen, BgBrush, FontBrush, MyFont);
                //    child.DrawSubtreeNodes(gr);*/

                }
            }
        }


        class CircleNode : IDrawable
        {
            // The string we will draw.
            public string Text;

            // Constructor.
            public CircleNode(string new_text)
            {
                Text = new_text;
            }
            public CircleNode()
            {

            }
            // Return the size of the string plus a 10 pixel margin.
            public SizeF GetSize(Graphics gr, Font font)
            {
                return gr.MeasureString(Text, font) + new SizeF(40, 10);
            }

            // Draw the object centered at (x, y).
            void IDrawable.Draw(float x, float y, Graphics gr, Pen pen, Brush bg_brush, Brush text_brush, Font font, int type)
            {
                // Fill and draw an ellipse at our location.
                SizeF my_size = GetSize(gr, font);
                RectangleF rect = new RectangleF(
                    x - my_size.Width / 2,
                    y - my_size.Height / 2,
                    my_size.Width, my_size.Height);
                gr.FillEllipse(bg_brush, rect);
                if (type == 0)
                {
                    System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Yellow);
                    gr.DrawEllipse(pen, rect);
                    gr.FillEllipse(myBrush, rect);
                }
                else
                {
                    gr.DrawRectangle(pen, rect.X, rect.Y, rect.Width, rect.Height);
                    System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.LightBlue);
                    gr.FillRectangle(myBrush, rect);
                }
                // Draw the text.
                using (StringFormat string_format = new StringFormat())
                {
                    string_format.Alignment = StringAlignment.Center;
                    string_format.LineAlignment = StringAlignment.Center;
                    gr.DrawString(Text, font, text_brush, x, y, string_format);
                }
            }
        }

        private TreeNode<CircleNode> root =
                new TreeNode<CircleNode>(new CircleNode());

        void program(ref int i)
        {
            Statement_sequence(ref i);
        }
        TreeNode<CircleNode> Statement_sequence(ref int i)
        {
            TreeNode<CircleNode> temp_root_old = new TreeNode<CircleNode>(new CircleNode());
            TreeNode<CircleNode> temp_root_new = new TreeNode<CircleNode>(new CircleNode());
            TreeNode<CircleNode> initial_temp = new TreeNode<CircleNode>(new CircleNode());
            initial_temp = Statement(ref i);
            temp_root_old = initial_temp;
            while (i < Globals.token_value.Count() && (Globals.token_value[i] == ";"))
            {
                i++;
                if (i == Globals.token_value.Count())
                {
                    Globals.list_of_errors += "Error ; after final statement\n";
                    Globals.Error = 1;
                }
                else
                {
                    temp_root_new = Statement(ref i);
                    temp_root_old.AddRightNeighbour(temp_root_new);
                    temp_root_old = temp_root_new;
                }
            }
            if (i < Globals.token_type.Count() && !(Globals.token_type[i] == "ELSE" || Globals.token_type[i] == "END" || Globals.token_type[i] == "UNTIL"))
            {

                Globals.list_of_errors += "Error Expected ; after statement\n";
                Globals.Error = 1;
            }
            return initial_temp;
        }
        TreeNode<CircleNode> Statement(ref int i)
        {
            TreeNode<CircleNode> temp_root = null;
            if (Globals.token_type[i] == "READ")
            {
                if (i == 0)
                {
                    root = Read(ref i);
                    temp_root = root;
                }
                else
                {
                    temp_root = Read(ref i);
                }
            }
            else if (Globals.token_type[i] == "IF")
            {
                if (i == 0)
                {
                    root = (IF(ref i));
                    temp_root = root;
                }
                else
                {
                    TreeNode<CircleNode> temp_root1 = IF(ref i);
                    // temp_root.AddChild(temp_root1);
                    temp_root = temp_root1;
                }
            }
            else if (Globals.token_type[i] == "REPEAT")
            {
                if (i == 0)
                {
                    root = repeat(ref i);
                    temp_root = root;
                }
                else
                {
                    TreeNode<CircleNode> temp_root1 = repeat(ref i);
                    //temp_root.AddChild(temp_root1);
                    temp_root = temp_root1;
                }

            }
            else if (Globals.token_type[i] == "WRITE")
            {
                if (i == 0)
                {
                    root = Write(ref i);
                    temp_root = root;
                }
                else
                {
                    TreeNode<CircleNode> temp_root1 = Write(ref i);
                    // temp_root.AddChild(temp_root1);
                    temp_root = temp_root1;
                }
            }
            else if (Globals.token_type[i] == "IDENTIFIER")
            {

                if (i == 0)
                {
                    i++;
                    root = Assign(ref i);
                    temp_root = root;
                }
                else
                {
                    i++;
                    TreeNode<CircleNode> temp_root1 = Assign(ref i);
                    //  temp_root.AddChild(temp_root1);
                    temp_root = temp_root1;
                }
            }

            return temp_root;
        }

        TreeNode<CircleNode> repeat(ref int i)
        {
            TreeNode<CircleNode> temp = new TreeNode<CircleNode>(new CircleNode("\n" + Globals.token_value[i] + "\n"), 1);
            i++;
            temp.AddChild(Statement_sequence(ref i));
            if (i < Globals.token_value.Count() && Globals.token_type[i] == "UNTIL")
            {
                i++;
                temp.AddChild(exp(ref i));
            }
            else
            {
                Globals.list_of_errors += "Error Expected until after repeat\n";
                Globals.Error = 1;
            }
            return temp;
        }
        TreeNode<CircleNode> Assign(ref int i)
        {
            TreeNode<CircleNode> temp = null;
            if (i < Globals.token_type.Count() && Globals.token_type[i] == "ASSIGN")
            {
                temp = new TreeNode<CircleNode>(new CircleNode("assign" + "\n" + "(" + Globals.token_value[i - 1] + ")"), 1);
                i++;
                temp.AddChild(exp(ref i));
                //  i++;
            }
            else
            {
                Globals.list_of_errors += "Error Expected ASSIGN\n";
                Globals.Error = 1;
            }
            return temp;
        }
        TreeNode<CircleNode> Write(ref int i)
        {
            TreeNode<CircleNode> temp = new TreeNode<CircleNode>(new CircleNode("\n" + Globals.token_value[i] + "\n"), 1);
            i++;
            temp.AddChild(exp(ref i));
            return temp;
        }


        TreeNode<CircleNode> IF(ref int i)
        {
            TreeNode<CircleNode> temp = new TreeNode<CircleNode>(new CircleNode("\n" + Globals.token_value[i] + "\n"), 1);
            i++;
            temp.AddChild(exp(ref i));
            if ((i < Globals.token_type.Count()) && Globals.token_type[i] == "THEN")
            {
                i++;
                temp.AddChild(Statement_sequence(ref i));
                if (i < Globals.token_type.Count() && Globals.token_type[i] == "ELSE")
                {
                    i++;
                    temp.AddChild(Statement_sequence(ref i));
                }

                if (i < Globals.token_type.Count() && Globals.token_type[i] == "END")
                {

                    i++;
                }
                else
                {
                    Globals.list_of_errors = "Expected 'end' after if statement\n";
                    Globals.Error = 1;
                }

            }
            else
            {
                Globals.list_of_errors = "Expected 'then' after if statement\n";
                Globals.Error = 1;
            }
            return temp;
        }

        TreeNode<CircleNode> Read(ref int i)
        {
            TreeNode<CircleNode> temp = new TreeNode<CircleNode>(new CircleNode(), 1);
            if (Globals.token_type[i + 1] == "IDENTIFIER")
            {
                temp = new TreeNode<CircleNode>(new CircleNode(Globals.token_value[i] + "\n" + "(" + Globals.token_value[i + 1] + ")"), 1);
            }
            else
                Globals.list_of_errors += "Error Expected Identifier\n";
            i += 2;
            return temp;
        }
        TreeNode<CircleNode> factor(ref int i)
        {
            TreeNode<CircleNode> temp = null;
            if (i < Globals.token_type.Count())
            {
                if (Globals.token_type[i] == "IDENTIFIER")
                {
                    temp = new TreeNode<CircleNode>(new CircleNode("id" + "\n" + "(" + Globals.token_value[i] + ")"), 0);
                    i++;
                }
                else if (Globals.token_type[i] == "NUMBER")
                {
                    temp = new TreeNode<CircleNode>(new CircleNode("const" + "\n" + "(" + Globals.token_value[i] + ")"), 0);
                    i++;
                }
                else if (Globals.token_type[i] == "OPENBRACKET")
                {
                    i++;
                    temp = exp(ref i);
                    if (Globals.token_type[i] == "CLOSEDBRACKET")
                        i++;
                }
                else
                {
                    Globals.list_of_errors += "Error Expected Identifier or Number or '(' \n";
                    Globals.Error = 1;
                }
            }
            else
            {
                Globals.list_of_errors += "Error Expected expression after assign  \n";
                Globals.Error = 1;

            }

            return temp;
        }


        TreeNode<CircleNode> term(ref int i)
        {
            TreeNode<CircleNode> temp = factor(ref i);
            TreeNode<CircleNode> new_temp = null;
            while (i < Globals.token_type.Count() && (Globals.token_type[i] == "MULT" || Globals.token_type[i] == "DIV"))
            {
                new_temp = new TreeNode<CircleNode>(new CircleNode("op" + "\n" + "(" + Globals.token_value[i] + ")"), 0);
                new_temp.AddChild(temp);
                i++;
                new_temp.AddChild(factor(ref i));
                //  i++;
                temp = new_temp;
            }
            return temp;
        }



        TreeNode<CircleNode> exp(ref int i)
        {
            TreeNode<CircleNode> temp = simple_exp(ref i);
            TreeNode<CircleNode> new_temp = null;
            if (i < Globals.token_type.Count() && (Globals.token_type[i] == "LESSTHAN" || Globals.token_type[i] == "EQUAL"))
            {
                new_temp = new TreeNode<CircleNode>(new CircleNode("op" + "\n" + "(" + Globals.token_value[i] + ")"), 0);
                new_temp.AddChild(temp);
                i++;
                new_temp.AddChild(simple_exp(ref i));
                //    i++;
                temp = new_temp;
            }
            return temp;
        }

        TreeNode<CircleNode> simple_exp(ref int i)
        {
            TreeNode<CircleNode> temp = term(ref i);
            TreeNode<CircleNode> new_temp = null;
            while (i < Globals.token_type.Count() && (Globals.token_type[i] == "PLUS" || Globals.token_type[i] == "MINUS"))
            {
                new_temp = new TreeNode<CircleNode>(new CircleNode("op" + "\n" + "(" + Globals.token_value[i] + ")"), 0);
                new_temp.AddChild(temp);
                i++;
                new_temp.AddChild(term(ref i));
                // i++;
                temp = new_temp;
            }

            return temp;
        }














        public static class Globals
        {
            public static string[] lines_of_text;
            public static List<string> token_value = new List<string>();
            public static List<string> token_type = new List<string>();
            public static string alltext;
            public static int first_error = -1;
            //public static List<List<List<Microsoft.VisualBasic.PowerPacks.LineShape>>> lines = new List<List<List<Microsoft.VisualBasic.PowerPacks.LineShape>>>();
            public static List<List<Rectangle>> rectangles = new List<List<Rectangle>>();
            // public static List<drawn_object> drawn_objects = new List<drawn_object>();
            public static int repeat_until = 1;
            public static int Error = 0;
            public static string list_of_errors = "";
            public static int picture_box_max_x = 0;
            public static int picture_box_max_y = 0;

        }

        private void Draw_Click(object sender, EventArgs e)
        {

            if ((InputDirectly.Checked == true && String.IsNullOrEmpty(InputTextBox.Text)))
            {
                MessageBox.Show("Empty! Please Enter TINY grammar rules ");

            }
            else if ((InputTextFile.Checked == true && String.IsNullOrEmpty(Globals.alltext)))
            {
                MessageBox.Show("The text file you choose is Empty! Please choose the correct TINY grammar rules file ");
                Browse.Enabled = true;
            }
            else
            {
                if (index < listPanel.Count - 1)
                    listPanel[++index].BringToFront();


                if (InputDirectly.Checked == true && !String.IsNullOrEmpty(InputTextBox.Text))
                    Globals.lines_of_text = InputTextBox.Text.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                Globals.token_value.Clear();
                Globals.token_type.Clear();
                for (int i = 0; i < Globals.lines_of_text.Count(); i++)
                {
                    if (Globals.lines_of_text[i] == "{")
                    {
                        while (Globals.lines_of_text[i] != "}") i++;
                    }
                    else
                    {
                        Globals.token_value.Add(Globals.lines_of_text[i].Substring(0, Globals.lines_of_text[i].IndexOf(',')).Trim());
                        Globals.token_type.Add(Globals.lines_of_text[i].Substring(Globals.lines_of_text[i].IndexOf(',') + 1).Trim());
                    }
                }

                int i1 = 0;
                root = null;
                program(ref i1);
                pictureBox1.Image = null;
                if (Globals.Error == 0)
                    ArrangeTree();
                else
                    MessageBox.Show(Globals.list_of_errors);

            }
        }

        private void Parser_Load(object sender, EventArgs e)
        {
            listPanel.Add(panel1);
            listPanel.Add(panel2);
            panel3.Controls.Add(pictureBox1);
            listPanel[index].BringToFront();
            Browse.Enabled = false;
            Draw.Enabled = false;
            Restart.Enabled = false;
            BrowseTextBox.Enabled = false;
            InputTextBox.Enabled = false;
            InputTextBox.ScrollBars = ScrollBars.Both;
            InputTextBox.WordWrap = false;
            panel2.AutoScroll = true;
            pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;



        }
        private void ArrangeTree()
        {
            using (Graphics gr = this.CreateGraphics())
            {
                // Arrange the tree once to see how big it is.
                float xmin = 0, ymin = 0;
                root.Arrange(gr, ref xmin, ref ymin);

                // Arrange the tree again to center it.
                xmin = (this.ClientSize.Width - xmin) / 2;
                ymin = (this.ClientSize.Height - ymin) / 2;
                root.Arrange(gr, ref xmin, ref ymin);
            }

            // Redraw.
            this.Refresh();
        }
        private void Restart2_Click(object sender, EventArgs e)
        {
            if (index > 0)
                listPanel[--index].BringToFront();
            Browse.Enabled = false;
            Draw.Enabled = false;
            Restart.Enabled = false;
            BrowseTextBox.Enabled = false;
            InputTextBox.Enabled = false;
            InputTextFile.Checked = false;
            InputDirectly.Checked = true;
            BrowseTextBox.Text = "";
            Globals.alltext = "";
            InputTextFile.Enabled = true;
            InputDirectly.Enabled = true;
            Confirm.Enabled = true;
            Globals.Error = 0;
            Globals.list_of_errors = "";
        }



        Pen blackPen = new Pen(Color.Black, 1);
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Size size = pictureBox1.Size;
            size.Height = 4000;
            size.Width = 12000;
            pictureBox1.Size = size;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
            if (Globals.Error == 0)
            {
                root.DrawTree(e.Graphics);
            }

        }



        private void Confirm_Click(object sender, EventArgs e)
        {
            if (InputDirectly.Checked == true)
            {
                InputTextBox.Enabled = true;
                Draw.Enabled = true;
            }
            else if (InputTextFile.Checked == true)
            {
                BrowseTextBox.Enabled = true;
                Browse.Enabled = true;
            }
            Restart.Enabled = true;
            InputTextFile.Enabled = false;
            InputDirectly.Enabled = false;
            Confirm.Enabled = false;
        }

        private void Restart_Click(object sender, EventArgs e)
        {
            Browse.Enabled = false;
            Draw.Enabled = false;
            Restart.Enabled = false;
            BrowseTextBox.Enabled = false;
            InputTextBox.Enabled = false;
            InputTextFile.Checked = false;
            InputDirectly.Checked = true;
            BrowseTextBox.Text = "";
            Globals.alltext = "";
            InputTextFile.Enabled = true;
            InputDirectly.Enabled = true;
            Confirm.Enabled = true;
            Globals.Error = 0;
            Globals.list_of_errors = "";
        }

        private void Browse_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Text Documents|*.txt";
            openFileDialog1.Title = "Open txt";
            openFileDialog1.CheckFileExists = true;
            openFileDialog1.CheckPathExists = true;
            openFileDialog1.FileName = "Code";
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Browse.Enabled = false;
                //MessageBox.Show(openFileDialog1.FileName);
                BrowseTextBox.Text = openFileDialog1.FileName;
                Draw.Enabled = true;
                var sr = new StreamReader(openFileDialog1.FileName);
                Globals.alltext = File.ReadAllText(openFileDialog1.FileName);
                Globals.lines_of_text = File.ReadAllLines(openFileDialog1.FileName);
            }
        }

        private void SaveImage_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFile = new SaveFileDialog();
            saveFile.InitialDirectory = @"C:\Users\Baher\Desktop";
            saveFile.Filter = "Image Files(*.jpg,*.png,*.tiff,*.bmp,*.gif)|*.jpg;*.png;*.tiff;*.bmp;*.gif";
            saveFile.Title = "Save an image";
            saveFile.AddExtension = true;
            saveFile.DefaultExt = "bmp";
            saveFile.FilterIndex = 2;
            saveFile.RestoreDirectory = true;
            if (saveFile.ShowDialog() == DialogResult.OK)
            {
                Bitmap bmp = new Bitmap((int)(pictureBox1.ClientSize.Width), (int)pictureBox1.ClientSize.Height);
                pictureBox1.DrawToBitmap(bmp, pictureBox1.ClientRectangle);
                string fName = saveFile.FileName;
                bmp.Save(fName, ImageFormat.Bmp);
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

    }
}
