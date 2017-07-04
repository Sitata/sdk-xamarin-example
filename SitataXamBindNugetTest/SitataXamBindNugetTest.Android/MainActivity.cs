using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Threading.Tasks;
using Com.Sitata.Sdk.Core.Models;
using System.Collections.Generic;
using Java.Lang;
using Retrofit2;

namespace SitataXamBindNugetTest.Droid
{


    class MyCountryCallback : Java.Lang.Object, ICallback {

        public void OnFailure( ICall p0, Throwable p1 ) {
            System.Diagnostics.Debug.WriteLine( $"===> OnFailure" );
        }

        public void OnResponse( ICall p0, Response p1 ) {
            System.Diagnostics.Debug.WriteLine( $"===> OnResponse got code {p1.Code()}" );
            if ( p1.IsSuccessful ) {
                var c = p1.Body() as Country;
                System.Diagnostics.Debug.WriteLine( $"===> OnResponse got country: {c?.Name}" );
            }
        }

    }


    [Activity (Label = "SitataXamBindNugetTest.Android", MainLauncher = true, Icon = "@drawable/icon")]
	public class MainActivity : Activity
	{

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

            Button btnLaunchAlerts = FindViewById<Button>( Resource.Id.btn_launch_alerts );
            Button btnLaunchAdvisories = FindViewById<Button>( Resource.Id.btn_launch_advisories );
            Button btnLaunchVaccinations = FindViewById<Button>( Resource.Id.btn_launch_vaccinations );
            Button btnLaunchMedications = FindViewById<Button>( Resource.Id.btn_launch_medications );
            Button btnLaunchDiseases = FindViewById<Button>( Resource.Id.btn_launch_diseases );
            Button btnLaunchHospitals = FindViewById<Button>( Resource.Id.btn_launch_hospitals );
            Button btnLaunchSafety = FindViewById<Button>( Resource.Id.btn_launch_safety );
            Button btnLaunchEmergNums = FindViewById<Button>( Resource.Id.btn_launch_emerg_nums );
            //Button btnLaunchTripBuilder = FindViewById<Button>( Resource.Id.btn_launch_trip_builder );
            Button btnLaunchTests = FindViewById<Button>( Resource.Id.btn_launch_tests );

            btnLaunchAlerts.Click += delegate { if ( MyApp.SitataReady ) MyApp.SitataGui.LaunchAlerts( this ); };
            btnLaunchAdvisories.Click += delegate { if ( MyApp.SitataReady ) MyApp.SitataGui.LaunchAdvisories( this ); };
            btnLaunchVaccinations.Click += delegate { if ( MyApp.SitataReady ) MyApp.SitataGui.LaunchVaccinations( this ); };
            btnLaunchMedications.Click += delegate { if ( MyApp.SitataReady ) MyApp.SitataGui.LaunchMedications( this ); };
            btnLaunchDiseases.Click += delegate { if ( MyApp.SitataReady ) MyApp.SitataGui.LaunchDiseases( this ); };
            btnLaunchHospitals.Click += delegate { if ( MyApp.SitataReady ) MyApp.SitataGui.LaunchHospitals( this ); };
            btnLaunchSafety.Click += delegate { if ( MyApp.SitataReady ) MyApp.SitataGui.LaunchSafety( this ); };
            btnLaunchEmergNums.Click += delegate { if ( MyApp.SitataReady ) MyApp.SitataGui.LaunchEmergencyNumbers( this ); };
            //btnLaunchTripBuilder.Click += delegate { if (MyApp.SitataReady) MyApp.SitataGui.LaunchTripBuilder( this ); };

            btnLaunchTests.Click += delegate {
                if ( MyApp.SitataReady ) {

                    // DB

                    var alerts = MyApp.SitataGui.Ctrlr.Db.Alerts.FindCurrent();

                    foreach (var x in alerts) {
                        System.Diagnostics.Debug.WriteLine( $"===> alert dict item {x}" );
                    }

                    // return type `Android.Runtime.JavaList`, cast each element manually later
                    var someCountries = MyApp.SitataGui.Ctrlr.Db.Countries.QueryBuilder()
                        .Where( CountryDao.Properties.TravelStatus.Eq( 0 ) )
                        .Limit( 3 )
                        .List();

                    var country = someCountries.Count == 0 ? null : someCountries[0] as Country;

                    System.Diagnostics.Debug.WriteLine( $"===> some country: {country?.Name}" );

                    // API -- NOTE: currently broken

                    Task.Run( () => {

                        var cid = country?.Id;

                        // asynchronous

                        //MyApp.SitataGui.Ctrlr.Requests.Alerts.GetForCountry( cid ).Enqueue( new MyCountryCallback() );

                        // synchronous

                        //Retrofit2.ICall call = MyApp.SitataGui.Ctrlr.Requests.Alerts.GetForCountry( cid );
                        //Retrofit2.Response resp = call.Execute();

                        //System.Diagnostics.Debug.WriteLine( $"===> got code {resp.Code()} for country {cid}" );

                    } );

                    // Jobs

                    MyApp.SitataGui.Ctrlr.Jobs.Advisories.Current.ReplaceAndStart();

                }
            };

        }
    }
}


