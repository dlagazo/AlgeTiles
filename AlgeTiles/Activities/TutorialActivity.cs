using System;
using System.Collections.Generic;

using Android.App;
using Android.OS;
using Android.Views;
using Android.Support.V4.View;
using Android.Support.V4.App;
using Java.Lang;
using Android.Util;

//https://www.youtube.com/watch?v=EBmBKivPVX4
//Use video view: http://stackoverflow.com/questions/37942711/play-video-inside-view-pager
//http://stackoverflow.com/questions/30151480/how-play-video-on-videoview-inside-viewpager-from-server
//http://www.androidbegin.com/tutorial/android-video-streaming-videoview-tutorial/
namespace AlgeTiles.Activities
{
	[Activity(Label = "TutorialActivity", ScreenOrientation = Android.Content.PM.ScreenOrientation.Landscape)]
	public class TutorialActivity : FragmentActivity, ViewPager.IOnPageChangeListener
	{
		public static ViewPager _viewPager;

		public void OnPageScrolled(int position, float positionOffset, int positionOffsetPixels)
		{
			throw new NotImplementedException();
		}

		public void OnPageScrollStateChanged(int state)
		{
			throw new NotImplementedException();
		}

		public void OnPageSelected(int position)
		{
			throw new NotImplementedException();
		}

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			Window.AddFlags(WindowManagerFlags.Fullscreen);
			Window.ClearFlags(WindowManagerFlags.ForceNotFullscreen);
			ActionBar.Hide();

			// Create your application here
			SetContentView(Resource.Layout.Tutorial);

			_viewPager = FindViewById<ViewPager>(Resource.Id.pager);
			_viewPager.OffscreenPageLimit = 1;
			_viewPager.Adapter = new TutorialFragmentAdapter(SupportFragmentManager);
			//_viewPager.SetPageTransformer(true, new FadeTransformer());
		}
	}

	public class TutorialFragmentAdapter : FragmentStatePagerAdapter
	{
		private int[] tutorialPages =
		{
			Resource.Drawable.slide1,
			Resource.Drawable.slide2,
			Resource.Drawable.slide3,
			Resource.Drawable.slide4,
			Resource.Drawable.slide5,
			Resource.Drawable.slide6,
			Resource.Drawable.slide7,
			Resource.Drawable.slide8,
			Resource.Raw.var1,
			Resource.Raw.var2
		};


		private List<Android.Support.V4.App.Fragment> tutorialPagesFragments { get; set; }

		public TutorialFragmentAdapter(Android.Support.V4.App.FragmentManager fm) : base(fm)
		{
			tutorialPagesFragments = new List<Android.Support.V4.App.Fragment>
			{
				new TutorialFragment(tutorialPages[0]),
				new TutorialFragment(tutorialPages[1]),
				new TutorialFragment(tutorialPages[2]),
				new TutorialFragment(tutorialPages[3]),
				new TutorialFragment(tutorialPages[4]),
				new TutorialFragment(tutorialPages[5]),
				new TutorialFragment(tutorialPages[6]),
				new TutorialFragment(tutorialPages[7]),
				new VideoFragment(tutorialPages[8]),
				new VideoFragment(tutorialPages[9])
			};
		}

		#region implemented abstract members of PagerAdapter
		public override int Count
		{
			get
			{
				return tutorialPagesFragments.Count;
			}
		}
		#endregion

		#region implemented abstract members of FragmentStatePagerAdapter
		public override Android.Support.V4.App.Fragment GetItem(int position)
		{
			return tutorialPagesFragments[position];
		}
		#endregion
	}


	public class FadeTransformer : Java.Lang.Object, ViewPager.IPageTransformer
	{
		private const float MaxAngle = 30F;
		public void TransformPage(View view, float position)
		{
			if (position < -1 || position > 1)
			{
				view.Alpha = 0; // The view is offscreen.
			}
			else
			{
				view.Alpha = 1;

				view.PivotY = view.Height / 2; // The Y Pivot is halfway down the view.

				// The X pivots need to be on adjacent sides.
				if (position < 0)
				{
					view.PivotX = view.Width;
				}
				else
				{
					view.PivotX = 0;
				}

				view.RotationY = MaxAngle * position; // Rotate the view.
			}
		}
	}
}