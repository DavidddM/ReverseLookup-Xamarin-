﻿using Android.App;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Text;
using System.Collections.Specialized;

namespace ReverseLookup
{
    [Activity(Label = "ნომრების ბაზა", MainLauncher = true)]
    public class MainActivity : Activity
    {
        private readonly System.Uri API = new System.Uri("http://simpleapi.info/apps/numbers-info/info.php?results=json");

        private void ChangeText(string str)
        {
            TextView tw = FindViewById<TextView>(Resource.Id.textView1);
            tw.Text = str;
        }

        private string GetNumber()
        {
            EditText editText = FindViewById<EditText>(Resource.Id.editText1);
            return editText.Text;
        }

        private T GetData<T>() where T : new()
        {
            dynamic data = new T();
            data["number"] = GetNumber();
            return data;
        }

        private string GetJSONString()
        {
            return $"{{\"number\":\"{GetNumber()}\"}}";
        }

        private List<string> ParseData(string jsonString)
        {
            jsonString = jsonString.Remove(0, jsonString.IndexOf("{"));
            APIResponse respo = JsonConvert.DeserializeObject<APIResponse>(jsonString);

            if (respo.Success) return respo.Items;
            return null;
        }

        private async void SearchButtonClick(object sender, System.EventArgs e)
        {
            APIClient client = new APIClient(API);
            var data = GetData<NameValueCollection>();

            client.AsyncPost(data);
            var response = await client.Response;

            var names = ParseData(Encoding.ASCII.GetString(response));
            if (names is null)
            {
                ChangeText("სამწუხაროდ ვერაფერი ვერ მოიძებნა..");
                return;
            }

            string str = "";
            foreach (var name in names)
            {
                str += name + "\n";
            }
            ChangeText(str);
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            Button myButt = FindViewById<Button>(Resource.Id.button1);
            myButt.Click += SearchButtonClick;
        }
    }
}

