/*
 * MIT License
 *
 * Copyright (c) 2020 The Mauzo Team
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */

namespace Desktop.Templates
{
    /// <summary>
    /// Template de informe.
    /// </summary>
    /// <remarks>@ant04x - Antonio Izquierdo</remarks>
    class Inform
    {
        private int id;
        private int nSales;
        private int nRefunds;
        private int nDiscounts;

        private long dStart;
        private long dEnd;

        /// <summary>
        /// Propiedad que hace un get o un set sobre el atributo id.
        /// </summary>
        public int Id
        {
            get => id;
            set => id = value;
        }

        /// <summary>
        /// Propiedad que hace un get o un set sobre el atributo nSales.
        /// </summary>
        public int NSales 
        {
            get => nSales;
            set => nSales = value;
        }

        /// <summary>
        /// Propiedad que hace un get o un set sobre el atributo nRefunds.
        /// </summary>
        public int NRefunds
        {
            get => nRefunds;
            set => nRefunds = value;
        }

        /// <summary>
        /// Propiedad que hace un get o un set sobre el atributo nDiscounts.
        /// </summary>
        public int NDiscounts 
        {
            get => nDiscounts;
            set => nDiscounts = value;
        }

        /// <summary>
        /// Propiedad que hace un get o un set sobre el atributo dStart.
        /// </summary>
        public long DStart
        {
            get => dStart;
            set => dStart = value;
        }

        /// <summary>
        /// Propiedad que hace un get o un set sobre el atributo dEnd.
        /// </summary>
        public long DEnd
        {
            get => dEnd;
            set => dEnd = value;
        }
    }
}
