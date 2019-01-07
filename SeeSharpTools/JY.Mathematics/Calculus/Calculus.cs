namespace SeeSharpTools.JY.Mathematics
{
    /// <summary>
    /// 微分和积分类库
    /// </summary>
    public class Calculus
    {
        /// <summary>
        /// 一次微分，使用2nd order Central方法，数组长度需大于等于3
        /// </summary>
        /// <param name="x">原始数组</param>
        /// <param name="dt">间隔</param>
        /// <param name="y">一次微分数组</param>
        /// <param name="initialCondition">初始条件</param>
        /// <param name="finalCondition">结束条件</param>
        public static void Derivative_2ndOrderCentral(double[] x, double dt, ref double[] y, double initialCondition, double finalCondition)
        {
            Engine.Base.Derivative_2ndOrderCentral(x, dt, ref y, initialCondition, finalCondition);
        }

        /// <summary>
        /// 一次微分，使用4th order Central方法，数组长度需大于等于5。
        /// </summary>
        /// <param name="x">原始数组</param>
        /// <param name="dt">间隔</param>
        /// <param name="y">一次微分数组</param>
        /// <param name="initialCondition">初始条件，长度为2，第一个元素代表x[i-2]，第二个元素代表x[i-1]</param>
        /// <param name="finalCondition">结束条件，长度为2，第一个元素代表x[i+1]，第二个元素代表x[i+2]</param>
        public static void Derivative_4thOrderCentral(double[] x, double dt, ref double[] y, double[] initialCondition, double[] finalCondition)
        {
            Engine.Base.Derivative_4thOrderCentral(x, dt, ref y, initialCondition, finalCondition);
        }

        /// <summary>
        /// 一次微分，使用Backward方法，数组长度需大于等于2。
        /// </summary>
        /// <param name="x">原始数组</param>
        /// <param name="dt">间隔</param>
        /// <param name="y">一次微分数组</param>
        /// <param name="initialCondition">初始条件</param>
        public static void Derivative_Backward(double[] x, double dt, ref double[] y, double initialCondition)
        {
            Engine.Base.Derivative_Backward(x, dt, ref y, initialCondition);
        }

        /// <summary>
        /// 一次微分，使用Forward方法，数组长度需大于等于2。
        /// </summary>
        /// <param name="x">原始数组</param>
        /// <param name="dt">间隔</param>
        /// <param name="y">一次微分数组</param>
        /// <param name="initialCondition">初始条件</param>
        public static void Derivative_Forward(double[] x, double dt, ref double[] y, double finalCondition)
        {
            Engine.Base.Derivative_Forward(x, dt, ref y, finalCondition);
        }

        /// <summary>
        /// 一次积分，使用bode方法，数组长度需大于等于5。
        /// </summary>
        /// <param name="x">原始数组</param>
        /// <param name="dt">间隔</param>
        /// <param name="y">一次微分数组</param>
        /// <param name="initialCondition">初始条件，长度为2，第一个元素代表x[i-2]，第二个元素代表x[i-1]</param>
        /// <param name="finalCondition">结束条件，长度为2，第一个元素代表x[i+1]，第二个元素代表x[i+2]</param>
        public static void Integral_Bode(double[] x, double dt, ref double[] y, double[] initialCondition, double[] finalCondition)
        {
            Engine.Base.Integral_Bode(x, dt, ref y, initialCondition, finalCondition);
        }

        /// <summary>
        /// 一次积分，使用Sipson's方法，数组长度需大于等于3。
        /// </summary>
        /// <param name="x">原始数组</param>
        /// <param name="dt">间隔</param>
        /// <param name="y">一次微分数组</param>
        /// <param name="initialCondition">初始条件</param>
        /// <param name="finalCondition">结束条件</param>
        public static void Integral_Simpsons(double[] x, double dt, ref double[] y, double initialCondition, double finalCondition)
        {
            Engine.Base.Integral_Simpsons(x, dt, ref y, initialCondition, finalCondition);
        }

        /// <summary>
        /// 一次积分，使用Sipson's 3/8方法，数组长度需大于等于4。
        /// </summary>
        /// <param name="x">原始数组</param>
        /// <param name="dt">间隔</param>
        /// <param name="y">一次微分数组</param>
        /// <param name="initialCondition">初始条件，长度为2，第一个元素代表x[i-2]，第二个元素代表x[i-1]</param>
        /// <param name="finalCondition">结束条件</param>
        public static void Integral_Simpsons38(double[] x, double dt, ref double[] y, double[] initialCondition, double finalCondition)
        {
            Engine.Base.Integral_Simpsons38(x, dt, ref y, initialCondition, finalCondition);
        }

        /// <summary>
        /// 一次积分，使用Trapezodial方法，数组长度需大于等于2。
        /// </summary>
        /// <param name="x">原始数组</param>
        /// <param name="dt">间隔</param>
        /// <param name="y">一次微分数组</param>
        /// <param name="initialCondition">初始条件</param>
        public static void Integral_Trapezodial(double[] x, double dt, ref double[] y, double initialCondition)
        {
            Engine.Base.Integral_Trapezodial(x, dt, ref y, initialCondition);
        }
    }
}