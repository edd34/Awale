using System;

namespace Awale_Console
{
    public class Network
    {
        public Network()
        {
        }
        /*
        public override string ToString()
        {
            string value = "";
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (i == 3 && j == 7)
                        value += checkerBoard[i, j].ToString();
                    else
                        value += checkerBoard[i, j].ToString() + ",";
                }
            }

            Console.WriteLine(value);
            return value;
        }

        public void ToBoard(string s)
        {
            int cpt = 0;
            //int count = 1000;
            string[] tmp = new string[10000];
            tmp = s.Split(',');
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Int32.TryParse(tmp[cpt].ToString(), out this.checkerBoard[i, j]);
                    cpt++;
                }
            }
        }*/
    }
}

