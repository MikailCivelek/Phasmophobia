using System;
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

namespace Phasmo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        enum Evidences { EMF5, Spirit_Box, Fingerprints, Ghost_Orb, Ghost_Writing, Freezing_Temperatures, DOTS_Projector}
        private string _FirstEvidence = "";
        private string _SecondEvidence = "";
        private string _ThirdEvidence = "";
        private List<string> Evidence = new List<string>() { "EMF5", "Spirit Box", "Fingerprints", "Ghost Orb", "Ghost Writing", "Freezing Temperatures", "D.O.T.S Projector" };
        private List<string> EvidenceNotNeededHelper;
        private List<GhostType> Ghosts = new List<GhostType>();
        private List<GhostType> Potential;
        private string[] Missing;

        public MainWindow()
        {
            InitializeComponent();

            // ADDING ALL GHOSTS TO LIST

            Ghosts.Add(new GhostType("Spirit", "EMF5", "Spirit Box", "Ghost Writing", "Smudge sticks stop attacks for extra 90 seconds", "None"));
            Ghosts.Add(new GhostType("Wraith", "EMF5", "Spirit Box", "D.O.T.S Projector", "Toxic reaction to salt", "Can't be tracked by footprints"));
            Ghosts.Add(new GhostType("Phantom", "Fingerprints", "Spirit Box", "D.O.T.S Projector", "Taking a photo will make it go away for a little while", "Sanity drops quickly by looking at it"));
            Ghosts.Add(new GhostType("Poltergeist", "Ghost Writing", "Spirit Box", "Fingerprints", "Ineffective in an empty room", "Can throw multiple objects at the same time"));

            Ghosts.Add(new GhostType("Banshee", "Fingerprints", "Ghost Orb", "D.O.T.S Projector", "Less aggressive around Crucifix", "Only targets one person at a time"));
            Ghosts.Add(new GhostType("Jinn", "EMF5", "Freezing Temperatures", "Fingerprints", "Turning off the power disables ability", "Travels at faster speed if victim is far away"));
            Ghosts.Add(new GhostType("Mare", "Ghost Orb", "Spirit Box", "Ghost Writing", "Turning on lights lowers chance of attack", "Increased chance of attack in the dark"));
            Ghosts.Add(new GhostType("Revenant", "Ghost Orb", "Ghost Writing", "Freezing Temperatures", "Hiding will make it move more slowly", "While hunting, it will travel faster"));

            Ghosts.Add(new GhostType("Shade", "EMF5", "Ghost Writing", "Freezing Temperatures", "Will not hunt when multiple players are around", "Being shy -> Hard to find"));
            Ghosts.Add(new GhostType("Demon", "Freezing Temperatures", "Ghost Writing", "Fingerprints", "Ouija board answers take less sanity (succesful)", "Will attack more often than any other ghost"));
            Ghosts.Add(new GhostType("Yurei", "Ghost Orb", "Freezing Temperatures", "D.O.T.S Projector", "Smudging its room keeps the ghost from wandering", "Strong effect on sanity"));
            Ghosts.Add(new GhostType("Oni", "EMF5", "Freezing Temperatures", "D.O.T.S Projector", "Very active -> Easy to identify", "More active when players are around"));

            Ghosts.Add(new GhostType("Yokai", "Ghost Orb", "Spirit Box", "D.O.T.S Projector", "While hunting, it can only hear voices near it", "Talking near the ghost makes it to hunt more often"));
            Ghosts.Add(new GhostType("Hantu", "Ghost Orb", "Freezing Temperatures", "Fingerprints", "Moves slower in warmer areas", "Attacks more often and moves faster when it is cold"));
            Ghosts.Add(new GhostType("Goryo", "EMF5", "D.O.T.S Projector", "Fingerprints", "D.O.T.S.-evidence/itself shows only on cam with nobody nearby", "Rarely seen far from place of death"));
            Ghosts.Add(new GhostType("Myling", "EMF5", "Ghost Writing", "Fingerprints", "Very vocal and active, silent during hunts", "Frequently makes paranormal sounds"));

            Ghosts.Add(new GhostType("Onryo", "Spirit Box", "Ghost Orb", "Freezing Temperatures", "Fears fire and less likely to hunt when threatened", "Extinguishing a flame can cause a hunt"));
            Ghosts.Add(new GhostType("The Twins", "EMF5", "Spirit Box", "Freezing Temperatures", "Often interacts at the same time", "Either one of them can start a hunt"));
            Ghosts.Add(new GhostType("Raiju", "EMF5", "Ghost Orb", "D.O.T.S Projector", "Disrupts electronic devices during hunts", "Siphons power from eletronic devices to move faster"));
            Ghosts.Add(new GhostType("Obake", "EMF5", "Fingerprints", "Ghost Orb", "Shapeshifting will leave behind unique evidence", "When intereacting with environment, it will rarely leave a trace"));

            Potential = new List<GhostType>(Ghosts);
            EvidenceNotNeededHelper = new List<string>();
            FirstEvidence.ItemsSource = Evidence;
            SecondEvidence.ItemsSource = Evidence;
            ThirdEvidence.ItemsSource = Evidence;
            OutputListView.ItemsSource = Potential;
            StrengthListView.ItemsSource = Potential;
            WeaknessListView.ItemsSource = Potential;
            MissingListView.ItemsSource = Missing;
            EvidenceNotNeededListView.ItemsSource = EvidenceNotNeededHelper;
        }

        private void FirstEvidence_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FirstEvidence.SelectedItem != null)
            {
                EvidenceNotNeededHelper.Remove(FirstEvidence.SelectedItem.ToString());
                _FirstEvidence = FirstEvidence.SelectedItem.ToString();
                SearchGhosts(_FirstEvidence);

            }

        }

        private void SecondEvidence_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SecondEvidence.SelectedItem != null)
            {
                EvidenceNotNeededHelper.Remove(SecondEvidence.SelectedItem.ToString());
                _SecondEvidence = SecondEvidence.SelectedItem.ToString();
                SearchGhosts(_SecondEvidence);
            }

        }

        private void ThirdEvidence_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ThirdEvidence.SelectedItem != null)
            {
                EvidenceNotNeededHelper.Remove(ThirdEvidence.SelectedItem.ToString());
                _ThirdEvidence = ThirdEvidence.SelectedItem.ToString();
                SearchGhosts(_ThirdEvidence);
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            //RESET UI

            FirstEvidence.UnselectAll();
            SecondEvidence.UnselectAll();
            ThirdEvidence.UnselectAll();
            _FirstEvidence = "";
            _SecondEvidence = "";
            _ThirdEvidence = "";
            Potential = new List<GhostType>(Ghosts);
            Missing = null;
            EvidenceNotNeededHelper = new List<string>();
            OutputListView.ItemsSource = Potential;
            StrengthListView.ItemsSource = Potential;
            WeaknessListView.ItemsSource = Potential;
            MissingListView.ItemsSource = Missing;
            EvidenceNotNeededListView.ItemsSource = EvidenceNotNeededHelper;
        }
        private void SearchGhosts(string evidence)
        {
            for (int i = Potential.Count - 1; i >= 0; i--)
            {
                Boolean Found = false;
                for (int j = 0; j < 3; j++)
                {
                    if (evidence == Potential[i].Evidence[j])
                    {
                        Found = true;
                    }
                }
                if (!Found)
                {
                    Potential.Remove(Potential[i]);
                }
            }
            StrengthListView.Items.Refresh();
            WeaknessListView.Items.Refresh();
            OutputListView.Items.Refresh();
            FindMissing();

        }
        private void FindMissing()
        {
            EvidenceNotNeededHelper = new List<string>(Evidence);
            Missing =new string[Potential.Count];
            for (int i = 0; i < Missing.Length; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (!(_FirstEvidence == Potential[i].Evidence[j] || _SecondEvidence == Potential[i].Evidence[j] || _ThirdEvidence == Potential[i].Evidence[j]))
                    {
                        if (Missing[i] == null)
                        {
                            Missing[i] = Potential[i].Evidence[j];
                        }
                        else
                        {
                            Missing[i] += " - " + Potential[i].Evidence[j];
                        }
                        string result = EvidenceNotNeededHelper.FirstOrDefault(stringToCheck => stringToCheck.Contains(Potential[i].Evidence[j]));


                        if (result != null)
                        {
                            EvidenceNotNeededHelper.Remove(result);
                        }


                    }
                }

            }
            FilterEvidence(_FirstEvidence);
            FilterEvidence(_SecondEvidence);
            FilterEvidence(_ThirdEvidence);
            MissingListView.ItemsSource = Missing;
            MissingListView.Items.Refresh();
            EvidenceNotNeededListView.ItemsSource = EvidenceNotNeededHelper;
            EvidenceNotNeededListView.Items.Refresh();
        }
        private void FilterEvidence(string test)
        {
            if (test != "")
            {
                string result = EvidenceNotNeededHelper.FirstOrDefault(stringToCheck => stringToCheck.Contains(test));
                if (result != "")
                {
                    EvidenceNotNeededHelper.Remove(result);
                }
            }

            
        }
    }
}
