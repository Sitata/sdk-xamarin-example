using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Com.Sitata.Sdk.Core;
using Com.Sitata.Sdk.Gui;

namespace SitataXamBindNugetTest.Droid {

    [Application]
    class MyApp : Application {


        internal static SitataGui SitataGui { get; private set; }
        internal static bool SitataReady { get; private set; }


        public MyApp( IntPtr handle, JniHandleOwnership transfer ) : base( handle, transfer ) { }


        public override void OnCreate() {
            base.OnCreate();

            // instantiate sitata sdk
            var cfg = new SitataConfig.Builder()
                        .SetApiEndpoint( "https://staging.sitata.com/api/v1/" )
                        .SetContext( this )
                        .SetToken( "TKN UGFydG5lcjo6RXh0ZXJuYWxUcmF2ZWxsZXJ8NTkwMTVkOTBhZGVlOGQ5MDE3ODQzYTNmfHNjYXlpUWJURE1WekFWVFY4dlhi" )
                        .Build();

            SitataGui = new SitataGui( cfg );
            SitataReady = true;

        }

    }

}