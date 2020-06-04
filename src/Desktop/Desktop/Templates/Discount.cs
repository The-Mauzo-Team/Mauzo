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
    /// Template de descuento.
    /// </summary>
    /// <remarks>@ant04x - Antonio Izquierdo</remarks>
    class Discount
    {
        private int id;
        private string code;
        private string desc;
        private float pricePerc;

        /// <summary>
        /// Propiedad que hace un get o un set sobre el atributo id.
        /// </summary>
        public int Id
        {
            get => id;
            set => id = value;
        }

        /// <summary>
        /// Propiedad que hace un get o un set sobre el atributo code.
        /// </summary>
        public string Code
        {
            get => code;
            set => code = value;
        }

        /// <summary>
        /// Propiedad que hace un get o un set sobre el atributo desc.
        /// </summary>
        public string Desc
        {
            get => desc;
            set => desc = value;
        }

        /// <summary>
        /// Propiedad que hace un get o un set sobre el atributo pricePerc.
        /// </summary>
        public float PricePerc
        {
            get => pricePerc;
            set => pricePerc = value;
        }
    }
}
