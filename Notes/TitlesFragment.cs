using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Media.Audiofx;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace Notes
{
    public class TitlesFragment : ListFragment
    {
        int selectedPlayId;
        bool showMultipleFragments;

        public TitlesFragment()
        {
            
        }
        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            ListAdapter = new ArrayAdapter<string>(Activity, Android.Resource.Layout.SimpleListItemActivated1, NotesData.Titles);

            if (savedInstanceState != null)
            {
                selectedPlayId = savedInstanceState.GetInt("current_play_id", 0);
            }

            var dialougeContainer = Activity.FindViewById(Resource.Id.playdialouge_container);
            showMultipleFragments = dialougeContainer != null && dialougeContainer.Visibility == ViewStates.Visible;

            if (showMultipleFragments)
            {
                ListView.ChoiceMode = ChoiceMode.Single;
                ShowPlayDialogue(selectedPlayId);
            }
        }

        public override void OnSaveInstanceState(Bundle outState)
        {
            base.OnSaveInstanceState(outState);
            outState.PutInt("current_play_id", selectedPlayId);
        }

        public override void OnListItemClick(ListView l, View v, int position, long id)
        {
            ShowPlayDialogue(position);
                
        }

        private void ShowPlayDialogue(int playId)
        {
            selectedPlayId = playId;
            if (showMultipleFragments)
            {
                ListView.SetItemChecked(selectedPlayId, true);

                var playDialougeFragment = FragmentManager.FindFragmentById(Resource.Id.playdialouge_container) as PlayDialogueFragment;

                if (playDialougeFragment == null || playDialougeFragment.PlayId != playId)
                {
                    var container = Activity.FindViewById(Resource.Id.playdialouge_container);
                    var dialougeFragment = PlayDialogueFragment.NewInstance(selectedPlayId);

                    FragmentTransaction fragmentTransaction = FragmentManager.BeginTransaction();
                    fragmentTransaction.Replace(Resource.Id.playdialouge_container, dialougeFragment);
                    fragmentTransaction.AddToBackStack(null);
                    fragmentTransaction.SetTransition(FragmentTransit.FragmentFade);
                    fragmentTransaction.Commit();
                } 
            }
            else
            {
                var intent = new Intent(Activity, typeof(PlayDialogueActivity));
                intent.PutExtra("current_play_id", playId);
                StartActivity(intent);
            }
        }
    }
}
