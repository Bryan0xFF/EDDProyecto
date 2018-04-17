﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ArbolB
{
    public static class Utilidades
    {

        #region Utilidades Texto

        // Para formatear los enteros en los archivos
        internal const int TextoEnteroTamaño = 11;
        private const string TextoEnteroFormato = "00000000000;-0000000000";
        internal const int TextoLlaveTamaño = 20;

        // El salto de lina "enter"
        internal const int TextoNuevaLineaTamaño = 2;
        internal const string TextoNuevaLinea = "\r\n";

        // Separador a usar en el nodo y caracter auxiliar para sustituirlo
        internal const char TextoSeparador = '|';
        internal const char TextoSustitutoSeparador = '?'; // puede ser cualquier caracter distinto de |

        internal static string FormatearEntero(int numero)
        {
            return numero.ToString(TextoEnteroFormato);
        }

        internal static string FormatearLlave(string llave)
        {
            return llave.PadLeft(20, 'x');
        }

        #endregion


        #region UTilidades Bytes

        internal const int BinarioCaracterTamaño = 1; // Debido al UTF32

        public static int countMovieYear = 0;
        public static int countMovieName = 0;
        public static int countMovieGenre = 0;

        public static int countDocumentaryYear = 0;
        public static int countDocumentaryName = 0;
        public static int countDocumentaryGenre = 0;

        public static int countShowYear = 0;
        public static int countShowName = 0;
        public static int countShowGenre = 0;


        internal static string ConvertirBinarioYTexto(byte[] datosBinario)
        {
            return Encoding.ASCII.GetString(datosBinario);
        }

        internal static byte[] ConvertirBinarioYTexto(string datosTexto)
        {
            return Encoding.ASCII.GetBytes(datosTexto);
        }

        #endregion


        #region UTilidades Enteros

        internal const int EnteroYEnterTamaño = (Utilidades.TextoEnteroTamaño + Utilidades.TextoNuevaLineaTamaño);
        internal const int EnteroYEnterBinarioTamaño = EnteroYEnterTamaño * Utilidades.BinarioCaracterTamaño;

        private static byte[] ConvertirEnteroYEnter(int numero)
        {
            return Utilidades.ConvertirBinarioYTexto(Utilidades.FormatearEntero(numero) + Utilidades.TextoNuevaLinea);
        }

        private static int ConvertirEnteroYEnter(byte[] buffer)
        {
            return Convert.ToInt32(Utilidades.ConvertirBinarioYTexto(buffer).Replace(Utilidades.TextoNuevaLinea, ""));
        }

        internal static int LeerEntero(FileStream archivo, int posicion)
        {
            if (archivo == null)
            {
                throw new ArgumentNullException("archivo");
            }

            if (posicion < 0)
            {
                throw new ArgumentOutOfRangeException("posicion");
            }

            try
            {
                byte[] buffer = new byte[EnteroYEnterBinarioTamaño];
                posicion = posicion * EnteroYEnterBinarioTamaño;
                archivo.Seek(posicion, SeekOrigin.Begin);
                archivo.Read(buffer, 0, EnteroYEnterBinarioTamaño);
                return ConvertirEnteroYEnter(buffer);
            }
            catch (Exception)
            {
                return Utilidades.ApuntadorVacio;
            }
        }

        internal static void EscribirEntero(FileStream archivo, int posicion, int numero)
        {
            if (archivo == null)
            {
                throw new ArgumentNullException("archivo");
            }

            if (posicion < 0)
            {
                throw new ArgumentOutOfRangeException("posicion");
            }

            byte[] buffer = ConvertirEnteroYEnter(numero);
            posicion = posicion * EnteroYEnterBinarioTamaño;
            archivo.Seek(posicion, SeekOrigin.Begin);
            archivo.Write(buffer, 0, EnteroYEnterBinarioTamaño);
        }

        #endregion


        #region Otros

        internal const int ApuntadorVacio = -1;

        #endregion

    }
}
