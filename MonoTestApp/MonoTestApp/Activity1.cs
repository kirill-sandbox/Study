using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Util;

namespace MonoTestApp
{
	[Activity (Label = "MonoTestApp", MainLauncher = true)]
	public class Activity1 : Activity
	{
		public class MyButton:Button
		{
			private int mX;
			private int mY;
			public int x {
				get{ return mX;}
				set{ mX = value;}
			}
			public int y {
				get{ return mY;}
				set{ mY = value;}
			}

			public MyButton(Context context):base(context)
			{

			}

		}

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.Main);

			userId = new TextView(this);
			gameStatus = new TextView(this);
			CreateLayout();
			game = new Game(UpdateCell, UpdatePlayerId);
		}

		private MyButton[,] button = new MyButton[Game.fieldWidth, Game.fieldHeight];
		private Game game;
		private TextView userId, gameStatus;

		private void UpdateCell (int x, int y, int playerId)
		{
			string outStr;
			if (playerId == 1) {
				outStr = "X";
			} else if (playerId == 2) {
				outStr = "0";
			} else {
				outStr = "  ";
			}
			button[x,y].SetText(outStr, TextView.BufferType.Normal);
		}

		private void UpdatePlayerId (int playerId)
		{
			string outStr;
			if (playerId == 1) {
				outStr = "X";
			} else {
				outStr = "0";
			}
			userId.SetText("Ходит игрок: " + outStr, TextView.BufferType.Normal);
		}

		private void CreateLayout ()
		{
			TableLayout layout = FindViewById<TableLayout>(Resource.Id.myTableLayout);
			TableRow raw;
			for (int i = 0; i < Game.fieldWidth; i++) {
				raw = new TableRow(this);
				for (int j = 0; j < Game.fieldWidth; j++) {
					button[i, j] = new MyButton(this);
					button[i, j].SetText("  ", TextView.BufferType.Normal);
					button[i, j].x = i;
					button[i, j].y = j;
					button[i, j].Click += delegate(object sender, EventArgs e) {
						try
						{
							game.Move(((MyButton)sender).x, ((MyButton)sender).y);
							game.GameIsOver();
							gameStatus.SetText(" ", TextView.BufferType.Normal);
						}
						catch(Game.IllegalMoveException ex)
						{
							gameStatus.SetText("Такой ход сделать нельзя", TextView.BufferType.Normal);
						}
						catch(Game.GameOverException ex)
						{
							string outStr = "";
							if(ex.whoWin == 1) {
								outStr = "X";
							} else {
								outStr = "0";
							}
							gameStatus.SetText("Игра окончена, " + outStr + " - победитель!", TextView.BufferType.Normal);
						}
					};
					raw.AddView(button[i,j]);
				}
				if(i == 0) {
					raw.AddView(userId);
				} else if(i == 1) {
					raw.AddView(gameStatus);
				}
				layout.AddView(raw);
			}
			/*raw = new TableRow(this);
			raw.AddView(userId);
			raw.AddView(gameStatus);
			layout.AddView(raw);*/
		}
	}
}


