using SeeSharpTools.JY.Mathematics.Interfaces;
using System;

namespace SeeSharpTools.JY.Mathematics.Provider
{
    public partial class ProviderBase : ICalculus
    {
        #region Derivative

        public virtual void Derivative_2ndOrderCentral(double[] x, double dt, ref double[] y, double initialCondition, double finalCondition)
        {
            if (x.Length != y.Length)
            {
                throw new Exception("The Length of x and y are not equal");
            }
            if (x.Length < 3)
            {
                throw new Exception("The Length of x should larger than 3");
            }

            for (int i = 0; i < x.Length; i++)
            {
                if (i == 0)
                {
                    y[i] = 1.0 / 2 / dt * (x[i + 1] - initialCondition);
                }
                else if (i >= x.Length - 1)
                {
                    y[i] = 1.0 / 2 / dt * (finalCondition - x[i - 1]);
                }
                else
                {
                    y[i] = 1.0 / 2 / dt * (x[i + 1] - x[i - 1]);
                }
            }
        }

        public virtual void Derivative_4thOrderCentral(double[] x, double dt, ref double[] y, double[] initialCondition, double[] finalCondition)
        {
            if (x.Length != y.Length)
            {
                throw new Exception("The Length of x and y are not equal");
            }
            if (initialCondition.Length != 2)
            {
                throw new Exception("The Length of initialCondition should be 2");
            }
            if (finalCondition.Length != 2)
            {
                throw new Exception("The Length of finalCondition should be 2");
            }
            if (x.Length < 5)
            {
                throw new Exception("The Length of x should larger than 5");
            }

            for (int i = 0; i < x.Length; i++)
            {
                if (i < 2)
                {
                    y[i] = 1.0 / 12 / dt * (-1.0 * x[i + 2] + 8 * x[i + 1] - 8 * initialCondition[1] + initialCondition[0]);
                }
                else if (i >= x.Length - 2)
                {
                    y[i] = 1.0 / 12 / dt * (-1.0 * finalCondition[1] + 8 * finalCondition[0] - 8 * x[i - 1] + x[i - 2]);
                }
                else
                {
                    y[i] = 1.0 / 12 / dt * (-1.0 * x[i + 2] + 8 * x[i + 1] - 8 * x[i - 1] + x[i - 2]);
                }
            }
        }

        public virtual void Derivative_Backward(double[] x, double dt, ref double[] y, double initialCondition)
        {
            if (x.Length != y.Length)
            {
                throw new Exception("The Length of x and y are not equal");
            }
            if (x.Length < 2)
            {
                throw new Exception("The Length of x should larger than 2");
            }
            for (int i = 0; i < x.Length; i++)
            {
                if (i == 0)
                {
                    y[i] = 1.0 / dt * (x[i] - initialCondition);
                }
                else if (i == x.Length - 1)
                {
                    y[i] = 1.0 / dt * (x[i] - x[i - 1]);
                }
                else
                {
                    y[i] = 1.0 / dt * (x[i] - x[i - 1]);
                }
            }
        }

        public virtual void Derivative_Forward(double[] x, double dt, ref double[] y, double finalCondition)
        {
            if (x.Length != y.Length)
            {
                throw new Exception("The Length of x and y are not equal");
            }
            if (x.Length < 2)
            {
                throw new Exception("The Length of x should larger than 2");
            }

            for (int i = 0; i < x.Length; i++)
            {
                if (i == 0)
                {
                    y[i] = 1.0 / dt * (x[i + 1] - x[i]);
                }
                else if (i == x.Length - 1)
                {
                    y[i] = 1.0 / dt * (finalCondition - x[i]);
                }
                else
                {
                    y[i] = 1.0 / dt * (x[i + 1] - x[i]);
                }
            }
        }

        #endregion Derivative

        #region Integral

        public virtual void Integral_Bode(double[] x, double dt, ref double[] y, double[] initialCondition, double[] finalCondition)
        {
            if (x.Length != y.Length)
            {
                throw new Exception("The Length of x and y are not equal");
            }
            if (initialCondition.Length != 2)
            {
                throw new Exception("The Length of initialCondition should be 2");
            }
            if (finalCondition.Length != 2)
            {
                throw new Exception("The Length of finalCondition should be 2");
            }

            if (x.Length < 5)
            {
                throw new Exception("The Length of x should larger than 5");
            }
            for (int i = 0; i < x.Length; i++)
            {
                y[i] = 0;

                for (int j = 0; j < i; j++)
                {
                    if (j < 2)
                    {
                        y[i] += 7 * initialCondition[0] + 32 * initialCondition[1] + 12 * x[j] + 32 * x[j + 1] + 7 * x[j + 2];
                    }
                    else if (j >= x.Length - 2)
                    {
                        y[i] += 7 * x[j - 2] + 32 * x[j - 1] + 12 * x[j] + 32 * finalCondition[0] + 7 * finalCondition[1];
                    }
                    else
                    {
                        y[i] += 7 * x[j - 2] + 32 * x[j - 1] + 12 * x[j] + 32 * x[j + 1] + 7 * x[j + 2];
                    }
                }
                y[i] = y[i] * dt / 90.0;
            }
        }

        public virtual void Integral_Simpsons(double[] x, double dt, ref double[] y, double initialCondition, double finalCondition)
        {
            if (x.Length != y.Length)
            {
                throw new Exception("The Length of x and y are not equal");
            }
            if (x.Length < 3)
            {
                throw new Exception("The Length of x should larger than 3");
            }
            for (int i = 0; i < x.Length; i++)
            {
                y[i] = 0;

                for (int j = 0; j < i; j++)
                {
                    if (j < 1)
                    {
                        y[i] += initialCondition + 4 * x[j] + x[j + 1];
                    }
                    else if (j >= x.Length - 1)
                    {
                        y[i] += x[j - 1] + 4 * x[j] + finalCondition;
                    }
                    else
                    {
                        y[i] += x[j - 1] + 4 * x[j] + x[j + 1];
                    }
                }
                y[i] = y[i] * dt / 6.0;
            }
        }

        public virtual void Integral_Simpsons38(double[] x, double dt, ref double[] y, double[] initialCondition, double finalCondition)
        {
            if (x.Length != y.Length)
            {
                throw new Exception("The Length of x and y are not equal");
            }
            if (initialCondition.Length != 2)
            {
                throw new Exception("The Length of initialCondition should be 2");
            }
            if (x.Length < 4)
            {
                throw new Exception("The Length of x should larger than 4");
            }
            for (int i = 0; i < x.Length; i++)
            {
                y[i] = 0;

                for (int j = 0; j < i; j++)
                {
                    if (j < 2)
                    {
                        y[i] += initialCondition[0] + 3 * initialCondition[1] + 3 * x[j] + x[j + 1];
                    }
                    else if (j >= x.Length - 1)
                    {
                        y[i] += x[j - 2] + 3 * x[j - 1] + 3 * x[j] + finalCondition;
                    }
                    else
                    {
                        y[i] += x[j - 2] + 3 * x[j - 1] + 3 * x[j] + x[j + 1];
                    }
                }
                y[i] = y[i] * dt / 8.0;
            }
        }

        public virtual void Integral_Trapezodial(double[] x, double dt, ref double[] y, double initialCondition)
        {
            if (x.Length != y.Length)
            {
                throw new Exception("The Length of x and y are not equal");
            }
            if (x.Length < 2)
            {
                throw new Exception("The Length of x should larger than 2");
            }
            for (int i = 0; i < x.Length; i++)
            {
                y[i] = 0;

                for (int j = 0; j < i; j++)
                {
                    if (j < 1)
                    {
                        y[i] += initialCondition + x[j];
                    }
                    else if (j >= x.Length - 1)
                    {
                        y[i] += x[j - 1] + x[j];
                    }
                    else
                    {
                        y[i] += x[j - 1] + x[j];
                    }
                }
                y[i] = y[i] * dt / 2.0;
            }
        }

        #endregion Integral
    }
}