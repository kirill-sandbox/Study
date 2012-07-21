using System;

namespace MonoTestApp
{
	public class Game
	{
		public class IllegalMoveException:Exception{}
		public class GameOverException:Exception
		{
			private int mWhoWin;
			public int whoWin {
				get{ return mWhoWin;}
				set{ mWhoWin = value;}
			}
		}
		public delegate void UpdateCell(int x, int y, int playerId);
		public delegate void UpdatePlayerId(int playerId);

		private UpdateCell updateCell;
		private UpdatePlayerId updatePlayerId;

		public const int fieldWidth = 5;
		public const int fieldHeight = 5;
		public const int countToWinCells = 4;
		private int[, ] field = new int[fieldWidth, fieldHeight];
		private int playerId = 1;

		private int WhoWin ()
		{
			//TODO: разбить по функциям и оптимизировать
			//TODO: переделать алгоритм расчёта диагоналей

			int cnt = 0, last = 0;
			for (int i = 0; i < fieldWidth; i ++) {
				for (int j = 0; j < fieldHeight; j ++) {
					if (field [i, j] != 0) {
						if (last == field [i, j]) {
							cnt++;
						} else {
							last = field [i, j];
							cnt = 1;
						}
						if (cnt == countToWinCells)
							return last;
					} else {
						last = 0;
						cnt = 0;
					}
				}
			}

			cnt = 0;
			last = 0;
			for (int i = 0; i < fieldHeight; i ++) {
				for (int j = 0; j < fieldWidth; j ++) {
					if (field [j, i] != 0) {
						if (last == field [j, i]) {
							cnt++;
						} else {
							last = field [j, i];
							cnt = 1;
						}
						if (cnt == countToWinCells)
							return last;
					} else {
						last = 0;
						cnt = 0;
					}
				}
			}

			last = 0;
			cnt = 0;
			int x = 0, y = 0;
			for (int i = 0; i < fieldWidth; i++) {
				x = i;
				y = 0;
				while (x < fieldWidth) {
					if(y >= fieldHeight)
						break;

					//
					if (field [x, y] != 0) {
						if (last == field [x, y]) {
							cnt++;
						} else {
							last = field [x, y];
							cnt = 1;
						}
						if (cnt == countToWinCells)
							return last;
					} else {
						last = 0;
						cnt = 0;
					}
					//
					x++;
					y++;
				}
			}

			last = 0;
			cnt = 0;
			x = 0;
			y = 0;
			for (int i = 0; i < fieldHeight; i++) {
				x = 0;
				y = i;
				while (y < fieldHeight) {
					if(x >= fieldWidth)
						break;

					//
					if (field [x, y] != 0) {
						if (last == field [x, y]) {
							cnt++;
						} else {
							last = field [x, y];
							cnt = 1;
						}
						if (cnt == countToWinCells)
							return last;
					} else {
						last = 0;
						cnt = 0;
					}
					//
					x++;
					y++;
				}
			}

			///////////////////
			last = 0;
			cnt = 0;
			x = 0;
			y = 0;
			for (int i = 0; i < fieldWidth; i++) {
				x = i;
				y = 0;
				while (y < fieldHeight) {
					if(x < 0)
						break;

					//
					if (field [x, y] != 0) {
						if (last == field [x, y]) {
							cnt++;
						} else {
							last = field [x, y];
							cnt = 1;
						}
						if (cnt == countToWinCells)
							return last;
					} else {
						last = 0;
						cnt = 0;
					}
					//
					x--;
					y++;
				}
			}

			///////////////////
			last = 0;
			cnt = 0;
			x = 0;
			y = 0;
			for (int i = 0; i < fieldHeight; i++) {
				x = 0;
				y = i;
				while (x < fieldWidth) {
					if(y < 0)
						break;

					//
					if (field [x, y] != 0) {
						if (last == field [x, y]) {
							cnt++;
						} else {
							last = field [x, y];
							cnt = 1;
						}
						if (cnt == countToWinCells)
							return last;
					} else {
						last = 0;
						cnt = 0;
					}
					//
					x++;
					y--;
				}
			}


			return 0;
		}

		public void GameIsOver ()
		{
			int whoWin = WhoWin ();
			if (whoWin != 0) {
				GameOverException gameOverException = new GameOverException();
				gameOverException.whoWin = whoWin;
				throw gameOverException;
			}
		}

		public void Move (int x, int y)
		{
			GameIsOver();
			if (this.field [x, y] != 0) {
				throw new IllegalMoveException ();
			} else {
				this.field [x, y] = playerId;
				NextPlayer ();
				Update ();
			}
		}

		private void Update ()
		{
			for (int i = 0; i < fieldWidth; i++) {
				for (int j = 0; j < fieldWidth; j++) {
					updateCell(i, j, field[i, j]);
				}
			}
			updatePlayerId(playerId);
		}

		private void NextPlayer ()
		{
			if (this.playerId == 1) {
				playerId = 2;
			} else {
				playerId = 1;
			}
		}

		public Game (UpdateCell updateCell, UpdatePlayerId updatePlayerId)
		{
			this.updatePlayerId = updatePlayerId;
			this.updateCell = updateCell;
			Update();
		}
	}
}

