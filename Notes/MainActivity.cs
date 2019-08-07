using System;
using System.Linq;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using AlertDialog = Android.App.AlertDialog;

namespace Notes
{
    [Activity(Label = "@string/app_name")]
    public class MainActivity : AppCompatActivity
    {

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.floating_action_button);
            fab.Click += delegate
            {
                var layoutInflater = LayoutInflater.From(this);
                var view = layoutInflater.Inflate(Resource.Layout.user_input_dialog, null);
                var alertBuilder = new Android.Support.V7.App.AlertDialog.Builder(this);
                alertBuilder.SetView(view);
                var userDataTitle = view.FindViewById<EditText>(Resource.Id.editTextViewTitle);
                var userDataDescription = view.FindViewById<EditText>(Resource.Id.editTextViewDescription);
                alertBuilder.SetCancelable(false)
                    .SetPositiveButton("Submit",
                        delegate
                        {
                            Toast.MakeText(this, "Submit Input: " + userDataTitle.Text, ToastLength.Long).Show();
                            NotesData.Titles.Add(userDataTitle.Text);
                            NotesData.Dialogue.Add(userDataDescription.Text);
                        })
                    .SetNegativeButton("Cancel", delegate
                    {
                        alertBuilder.Dispose();
                    });
                var dialog = alertBuilder.Create();
                dialog.Show();
            };
        }
        

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }
        
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}

