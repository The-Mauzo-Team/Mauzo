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
    class Refund
    {
        private int id;
        private long dateRefund;
        private int userId;
        private int saleId;

        /// <summary>
        /// Propiedad para hacer un getter o setter sobre el atributo id
        /// </summary>
        public int Id
        {
            get => id;
            set => id = value; 
        }

        /// <summary>
        /// Propiedad para hacer un getter o setter sobre el atributo DateRefund
        /// </summary>
        public long DateRefund
        {
            get => dateRefund;
            set => dateRefund = value;
        }

        /// <summary>
        /// Propiedad para hacer un getter o setter sobre el atributo UserId
        /// </summary>
        public int UserId
        {
            get => userId;
            set => userId = value;
        }

        /// <summary>
        /// Propiedad para hacer un getter o setter sobre el atributo SaleId
        /// </summary>
        public int SaleId
        {
            get => saleId;
            set => saleId = value;
        }

    }
}
