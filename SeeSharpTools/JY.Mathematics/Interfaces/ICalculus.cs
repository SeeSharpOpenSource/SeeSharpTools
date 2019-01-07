namespace SeeSharpTools.JY.Mathematics.Interfaces
{
    internal interface ICalculus
    {
        void Derivative_2ndOrderCentral(double[] x, double dt, ref double[] y, double initialCondition, double finalCondition);

        void Derivative_4thOrderCentral(double[] x, double dt, ref double[] y, double[] initialCondition, double[] finalCondition);

        void Derivative_Forward(double[] x, double dt, ref double[] y, double finalCondition);

        void Derivative_Backward(double[] x, double dt, ref double[] y, double initialCondition);

        void Integral_Trapezodial(double[] x, double dt, ref double[] y, double initialCondition);

        void Integral_Simpsons(double[] x, double dt, ref double[] y, double initialCondition, double finalCondition);

        void Integral_Simpsons38(double[] x, double dt, ref double[] y, double[] initialCondition, double finalCondition);

        void Integral_Bode(double[] x, double dt, ref double[] y, double[] initialCondition, double[] finalCondition);
    }
}