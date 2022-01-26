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
        private string _FirstEvidence = "";
        private string _SecondEvidence = "";
        private string _ThirdEvidence = ""; 
        private List<string> Evidences = new List<string>() { Evidence.EMF5, Evidence.SpiritBox, Evidence.Fingerprints, Evidence.GhostOrb, Evidence.GhostWriting, Evidence.FreezingTemperatures, Evidence.DotsProjector};
        private List<string> EvidenceNotNeeded;
        private List<GhostType> Ghosts = new List<GhostType>();
        private List<GhostType> Potential;
        private string[] Missing;
        public MainWindow()
        {
            InitializeComponent();

            // ADDING ALL GHOSTS TO LIST
            Ghosts.Add(new GhostType("Spirit", Evidence.EMF5, Evidence.SpiritBox, Evidence.GhostWriting, "Smudge sticks stop attacks for extra 90 seconds", "None", "Focus on non-hunt duration after smudging"));
            Ghosts.Add(new GhostType("Wraith", Evidence.EMF5, Evidence.SpiritBox, Evidence.DotsProjector, "Toxic reaction to salt", "Can't be tracked by footprints", "Focus on salt, try to get it to walk through it (leaving no footprints)"));
            Ghosts.Add(new GhostType("Phantom", Evidence.Fingerprints, Evidence.SpiritBox, Evidence.DotsProjector, "Taking a photo will make it disappear for a little while", "Sanity drops quickly by looking at it", "Hunt: flash visible every 1-2 seconds while normally every 0.3-1 seconds"));
            Ghosts.Add(new GhostType("Poltergeist", Evidence.GhostWriting, Evidence.SpiritBox, Evidence.Fingerprints, "Ineffective in an empty room", "Can throw multiple objects at the same time (decreases sanity)", "Throws multiple objects that also drop sanity"));

            Ghosts.Add(new GhostType("Banshee", Evidence.Fingerprints, Evidence.GhostOrb, Evidence.DotsProjector, "Crucifixes have additional two meter range", "Focuses on one person at a time", "Has a chance to make an unique shriek-like sound with parabolic mic"));
            Ghosts.Add(new GhostType("Jinn", Evidence.EMF5, Evidence.FreezingTemperatures, Evidence.Fingerprints, "Turning off the power disables ability", "Travels at faster speed if victim is far away", "Try to watch the travel speed during hunts (power on/off difference)"));
            Ghosts.Add(new GhostType("Mare", Evidence.GhostOrb, Evidence.SpiritBox, Evidence.GhostWriting, "Lights on lowers chance of attack", "Darkness increases chance of attack",  "Prefers light-breaking events and can turn off lights when turned on"));
            Ghosts.Add(new GhostType("Revenant", Evidence.GhostOrb, Evidence.GhostWriting, Evidence.FreezingTemperatures, "Hiding will make it move more slowly", "While hunting if line of sight, it will travel faster", "Try to watch the travel speed during hunts"));

            Ghosts.Add(new GhostType("Shade", Evidence.EMF5, Evidence.GhostWriting, Evidence.FreezingTemperatures, "Will not hunt when multiple players are around", "Being shy -> Hard to find", "Only hunts below an average sanity of 35%, lower than most other ghosts"));
            Ghosts.Add(new GhostType("Demon", Evidence.FreezingTemperatures, Evidence.GhostWriting, Evidence.Fingerprints, "Reduced sanity drain from various cursed possessions", "Will attack more often than any other ghost", "Loves to attack at any time"));
            Ghosts.Add(new GhostType("Yurei", Evidence.GhostOrb, Evidence.FreezingTemperatures, Evidence.DotsProjector, "Smudging its room keeps the ghost from wandering", "Strong effect on sanity", "A door near the Yurei will close when it uses its sanity drain ability"));
            Ghosts.Add(new GhostType("Oni", Evidence.EMF5, Evidence.FreezingTemperatures, Evidence.DotsProjector, "Very active -> Easy to identify", "More active when players are around", "Cannot perform smoke-type ghost events"));

            Ghosts.Add(new GhostType("Yokai", Evidence.GhostOrb, Evidence.SpiritBox, Evidence.DotsProjector, "While hunting, it can only hear voices near it", "Talking near the ghost makes it to hunt more often", "Good luck :)"));
            Ghosts.Add(new GhostType("Hantu", Evidence.GhostOrb, Evidence.FreezingTemperatures, Evidence.Fingerprints, "Moves slower in warmer areas", "Attacks more often and moves faster when it is cold", "Always freezing temp. / emits freezing breath if in room below 0°C"));
            Ghosts.Add(new GhostType("Goryo", Evidence.EMF5, Evidence.DotsProjector, Evidence.Fingerprints, "Rarely seen far from place of death", "D.O.T.S.-evidence shows only on camera", "Always D.O.T.S. / test D.O.T.S. with video camera & physically"));
            Ghosts.Add(new GhostType("Myling", Evidence.EMF5, Evidence.GhostWriting, Evidence.Fingerprints, "Very vocal and active, silent during hunts", "Frequently makes paranormal sounds", "Drop flashlight, footsteps can be heard same time as device interference"));

            Ghosts.Add(new GhostType("Onryo", Evidence.SpiritBox, Evidence.GhostOrb, Evidence.FreezingTemperatures, "Fears fire and less likely to hunt when threatened", "Extinguishing a flame can cause a hunt", "Play with fire to test"));
            Ghosts.Add(new GhostType("The Twins", Evidence.EMF5, Evidence.SpiritBox, Evidence.FreezingTemperatures, "Often interacts at the same time", "Either one of them can start a hunt", "No help yet"));
            Ghosts.Add(new GhostType("Raiju", Evidence.EMF5, Evidence.GhostOrb, Evidence.DotsProjector, "Disrupts electronic devices during hunts", "Siphons power from eletronic devices to move faster", "Walks fast around electronic equipment"));
            Ghosts.Add(new GhostType("Obake", Evidence.EMF5, Evidence.Fingerprints, Evidence.GhostOrb, "Shapeshifting will leave behind unique evidence", "When intereacting with environment, it will rarely leave a trace", "Always fingerprints / check for unique evidence"));

            Ghosts.Add(new GhostType("Mimic", Evidence.SpiritBox, Evidence.Fingerprints, Evidence.FreezingTemperatures, "Copies other ghosts their abilities", "Induces Ghost Orbs as fourth evidence", "Ghost orbs as fourth evidence"));

            // CLONE GHOSTS
            Potential = new List<GhostType>(Ghosts);

            // CHECK FOR NOT NEEDED EVIDENCE
            EvidenceNotNeeded = new List<string>();

            // SOURCEBINDS
            lstName.ItemsSource = Potential;
            FirstEvidence.ItemsSource = Evidences;
            SecondEvidence.ItemsSource = Evidences;
            ThirdEvidence.ItemsSource = Evidences;
            lstWeaknesses.ItemsSource = Potential;
            lstStrengths.ItemsSource = Potential;
            lstNightmare.ItemsSource = Potential;
            lstNotNeeded.ItemsSource = EvidenceNotNeeded;
        }

        private void FirstEvidence_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (FirstEvidence.SelectedItem != null)
            {
                EvidenceNotNeeded.Remove(FirstEvidence.SelectedItem.ToString());
                _FirstEvidence = FirstEvidence.SelectedItem.ToString();
                SearchGhostsThatArePossible(_FirstEvidence);
            }
            

        }

        private void SecondEvidence_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SecondEvidence.SelectedItem != null)
            {
                EvidenceNotNeeded.Remove(SecondEvidence.SelectedItem.ToString());
                _SecondEvidence = SecondEvidence.SelectedItem.ToString();
                SearchGhostsThatArePossible(_SecondEvidence);
            }

        }

        private void ThirdEvidence_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ThirdEvidence.SelectedItem != null)
            {
                EvidenceNotNeeded.Remove(ThirdEvidence.SelectedItem.ToString());
                _ThirdEvidence = ThirdEvidence.SelectedItem.ToString();
                SearchGhostsThatArePossible(_ThirdEvidence);
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
            EvidenceNotNeeded = new List<string>();
            lstName.ItemsSource = Potential;
            lstWeaknesses.ItemsSource = Potential;
            lstStrengths.ItemsSource = Potential;
            lstNightmare.ItemsSource = Potential;
            lstEvidenceNeeded.ItemsSource = Missing;
            lstNotNeeded.ItemsSource = EvidenceNotNeeded;
        }
        private void SearchGhostsThatArePossible(string evidence)
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
            lstWeaknesses.Items.Refresh();
            lstStrengths.Items.Refresh();
            lstNightmare.Items.Refresh();
            lstName.Items.Refresh();
            FindMissingEvidenceForGhostTypesAndNotNeededEvidence();
            FilterEvidenceThatIsFoundAlreadyForNotNeeded(_FirstEvidence);
            FilterEvidenceThatIsFoundAlreadyForNotNeeded(_SecondEvidence);
            FilterEvidenceThatIsFoundAlreadyForNotNeeded(_ThirdEvidence);
            lstNotNeeded.ItemsSource = EvidenceNotNeeded;
            lstNotNeeded.Items.Refresh();

        }
        private void FindMissingEvidenceForGhostTypesAndNotNeededEvidence()
        {
            EvidenceNotNeeded = new List<string>(Evidences);
            Missing = new string[Potential.Count];

            
            for (int i = 0; i < Missing.Length; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    // FIND ALL EVIDENCES THAT ARE MISSING FOR EACH GHOST TYPE AND ADD THEM TO A STRING
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

                        // IF EVIDENCE IS FOUND IN THE STRING, REMOVE IT FROM THE LIST BECAUSE IT IS POTENTIAL EVIDENCE
                        string result = EvidenceNotNeeded.FirstOrDefault(stringToCheck => stringToCheck.Contains(Potential[i].Evidence[j]));
                        if (result != null)
                        {
                            EvidenceNotNeeded.Remove(result);
                        }


                    }
                }

            }
            lstEvidenceNeeded.ItemsSource = Missing;
            lstEvidenceNeeded.Items.Refresh();
            
        }
        private void FilterEvidenceThatIsFoundAlreadyForNotNeeded(string evidence)
        {
            if (evidence != "")
            {
                string result = EvidenceNotNeeded.FirstOrDefault(stringToCheck => stringToCheck.Contains(evidence));
                if (result != "")
                {
                    EvidenceNotNeeded.Remove(result);
                }
            }

            
        }
    }
}
