using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections;

namespace FilmArchiv
{
    public partial class Form1 : Form
    {
        ArrayList liste = new ArrayList();
        ArrayList nummerierung = new ArrayList();
        String Kat;
        String Film;
        String StrGenre;
        String Format;
        String Filminfo;
        String Refresh1;
        int Zeilennummer;
        int counter;
        int listcounter;
        int edit;
        int vorwaerts;
        

        public Form1()
        {
            InitializeComponent();
            Daten_lesen();
        }

        private void Daten_lesen()
        {
            try
            {
                
                System.IO.StreamReader file = new System.IO.StreamReader("c:\\FilmArchiv\\index.dat", true);
                // Counter fuer Zeilennummern
                while (file.Peek() != -1)
                {
                    liste.Add(file.ReadLine());
                    counter++;
                }

                file.Close();
            }

            catch
            {
                counter = 0;
            }
        }

        private void NeuEintrag_Click(object sender, EventArgs e)
        {
            Titel.Clear();
            Titel.ReadOnly = false;
            Info.Clear();
            Info.ReadOnly = false;
            Zeilennummer = counter;
            ID.Text = Convert.ToString(Zeilennummer);
            MessageBox.Show("Eintrag eingeben,\n dann 'Eintrag speichern'.");

        }

        private void Medium_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void EintragSpeichern_Click(object sender, EventArgs e)
        {
            String Listadd;
            if (edit == 1 && Titel.ReadOnly == false)
            {

                Refresh1 = Titel.Text.Substring(0, 1);
                if (Refresh1 == "0" || Refresh1 == "1" || Refresh1 == "2" || Refresh1 == "3" || Refresh1 == "4" || Refresh1 == "5" || Refresh1 == "6" || Refresh1 == "7" || Refresh1 == "8" || Refresh1 == "9")
                {
                    Refresh1 = "#";
                }
                Film = Titel.Text;
                Kat = Film.Substring(0, 1);
                if (Kat == "0" || Kat == "1" || Kat == "2" || Kat == "3" || Kat == "4" || Kat == "5" || Kat == "6" || Kat == "7" || Kat == "8" || Kat == "9")
                {
                    Kat = "#";
                }
                StrGenre = Genre.Text;
                Format = Medium.Text;
                Filminfo = Info.Text;
                Zeilennummer = Convert.ToInt32(ID.Text);
                
                // in eine bestimmte zeile DER DATEI schreiben
                
                Listadd = String.Format("{0, -6}{1, -2}" + "{2, -30}" + "{3, -20}" + "{4, -10}" + "{5}", Zeilennummer, Kat, Film, StrGenre, Format, Filminfo);
                liste.RemoveAt(Zeilennummer);
                liste.Insert(Zeilennummer, Listadd);

                listcounter = liste.Count;
                File.Delete("c:\\FilmArchiv\\index.dat");
                System.IO.StreamWriter file = new System.IO.StreamWriter("c:\\FilmArchiv\\index.dat", true);
                vorwaerts = 0;
                do
                {
                    
                    file.WriteLine("{0}", liste[vorwaerts]);

                    vorwaerts++;
                } while (listcounter != vorwaerts);


                file.Close();
                MessageBox.Show("Eintrag gespeichert.");

                // wichtig
                edit = 0;
                suche(Refresh1);
                Titel.Clear();
                Info.Clear();
                Titel.ReadOnly = true;
                Info.ReadOnly = true;
                
            }
            else if (ID.Text == Convert.ToString(counter) && Titel.ReadOnly == false)
            {

                Refresh1 = Titel.Text.Substring(0, 1);
                if (Refresh1 == "0" || Refresh1 == "1" || Refresh1 == "2" || Refresh1 == "3" || Refresh1 == "4" || Refresh1 == "5" || Refresh1 == "6" || Refresh1 == "7" || Refresh1 == "8" || Refresh1 == "9")
                {
                    Refresh1 = "#";
                }
                Film = Titel.Text;
                Titel.ReadOnly = true;
                Info.ReadOnly = true;
                Kat = Film.Substring(0, 1);
                if (Kat == "0" || Kat == "1" || Kat == "2" || Kat == "3" || Kat == "4" || Kat == "5" || Kat == "6" || Kat == "7" || Kat == "8" || Kat == "9")
                {
                    Kat = "#";
                }
                StrGenre = Genre.Text;
                Format = Medium.Text;
                Filminfo = Info.Text;
                Zeilennummer = Convert.ToInt32(ID.Text);
                try
                {
                    DirectoryInfo info = Directory.CreateDirectory(@"C:\FilmArchiv\");
                }
                catch
                {
                    MessageBox.Show("Das benötigte Verzeichnis 'FilmArchiv' \n im Verzeichnis C: konnte nicht erstellt werden. \nBitte setzen Sie entsprechende Rechte.");
                }
                
                System.IO.StreamWriter file = new System.IO.StreamWriter("c:\\FilmArchiv\\index.dat", true);
                file.WriteLine("{0, -6}{1, -2}" + "{2, -30}" + "{3, -20}" + "{4, -10}" + "{5}", Zeilennummer, Kat, Film, StrGenre, Format, Filminfo);
                Listadd = String.Format("{0, -6}{1, -2}" + "{2, -30}" + "{3, -20}" + "{4, -10}" + "{5}", Zeilennummer, Kat, Film, StrGenre, Format, Filminfo);
                liste.Add(Listadd);
                counter++;
                file.Close();
                MessageBox.Show("Eintrag gespeichert.");


                suche(Refresh1);
                Titel.Clear();                
                Info.Clear();               
            }
            else
            {
                MessageBox.Show("Bitte zuerst auf 'Neuer Eintrag' klicken.");
            }
            
        }
        private void suche(string s)
        {
            listcounter = liste.Count - 1;
            TitelBox.Items.Clear();
            nummerierung.Clear();
            while (listcounter >= 0)
            {
                //MessageBox.Show(liste[listcounter].ToString().Substring(6, 1));
                if (liste[listcounter].ToString().Substring(6, 1).ToLower().Equals(s.ToLower()))
                {
                    TitelBox.Items.Add(liste[listcounter].ToString().Substring(8, 30));
                    nummerierung.Add(liste[listcounter].ToString().Substring(0, 6));
                }
                listcounter--;
            }
        }
        private void nummern_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            
            String s = btn.Name;
            if (s == "nummern")
            {
                s = "#";
            }
            suche(s);
        }

        private void A_Click(object sender, EventArgs e)
        {
            suche("A");
        }

        private void B_Click(object sender, EventArgs e)
        {
            suche("B");
        }

        private void C_Click(object sender, EventArgs e)
        {
            suche("C");
        }

        private void D_Click(object sender, EventArgs e)
        {
            suche("D");
        }

        private void E_Click(object sender, EventArgs e)
        {
            suche("E");
        }

        private void F_Click(object sender, EventArgs e)
        {
            suche("F");
        }

        private void G_Click(object sender, EventArgs e)
        {
            suche("G");
        }

        private void H_Click(object sender, EventArgs e)
        {
            suche("H");
        }

        private void I_Click(object sender, EventArgs e)
        {
            suche("I");
        }

        private void J_Click(object sender, EventArgs e)
        {
            suche("J");
        }

        private void K_Click(object sender, EventArgs e)
        {
            suche("K");
        }

        private void L_Click(object sender, EventArgs e)
        {
            suche("L");
        }

        private void M_Click(object sender, EventArgs e)
        {
            suche("M");
        }

        private void TitelBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i;
            Titel.Text = TitelBox.SelectedItem.ToString();
            for (i = 0; !nummerierung[TitelBox.SelectedIndex].ToString().Equals(liste[i].ToString().Substring(0, 6)); )
            {
                i++;
            }
            ID.Text = i.ToString();
            
            Genre.Text = liste[i].ToString().Substring(38, 20);
            Medium.Text = liste[i].ToString().Substring(58, 10);
            Info.Text = liste[i].ToString().Substring(68); 
        }

        private void EintragBearbeiten_Click(object sender, EventArgs e)
        {
            Titel.ReadOnly = false;
            Info.ReadOnly = false;
            edit = 1;
            MessageBox.Show("Eintrag bearbeiten,\n dann 'Eintrag speichern'.");
        }

        private void DelEintrag_Click(object sender, EventArgs e)
        {
            
            Refresh1 = Titel.Text.Substring(0, 1);
            if (Refresh1 == "0" || Refresh1 == "1" || Refresh1 == "2" || Refresh1 == "3" || Refresh1 == "4" || Refresh1 == "5" || Refresh1 == "6" || Refresh1 == "7" || Refresh1 == "8" || Refresh1 == "9")
            {
                Refresh1 = "#";
            }


            DialogResult result = MessageBox.Show("Eintrag löschen?", "Abfrage", MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {

                Zeilennummer = Convert.ToInt32(ID.Text);
                liste.RemoveAt(Zeilennummer);
                listcounter = liste.Count;
                File.Delete("c:\\FilmArchiv\\index.dat");
                System.IO.StreamWriter file = new System.IO.StreamWriter("c:\\FilmArchiv\\index.dat", true);
                vorwaerts = 0;
                do
                {

                    file.WriteLine("{0}", liste[vorwaerts]);

                    vorwaerts++;
                } while (listcounter != vorwaerts);


                file.Close();
                suche(Refresh1);
                MessageBox.Show("Eintrag wurde gelöscht.");
            }
            else
            {
                MessageBox.Show("Eintrag wurde nicht gelöscht.");
            }
        }
    }
}
