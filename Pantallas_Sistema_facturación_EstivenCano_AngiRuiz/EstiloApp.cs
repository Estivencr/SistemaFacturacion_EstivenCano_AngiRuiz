using System;
using System.Drawing;
using System.Windows.Forms;

namespace Pantallas_Sistema_facturación_EstivenCano_AngiRuiz
{
    /// <summary>
    /// Clase estática que centraliza toda la paleta de colores y la lógica
    /// de estilos de la aplicación. Llamar a AplicarEstilo(form) desde el
    /// evento Load de cada formulario es suficiente para obtener un diseño
    /// consistente en toda la aplicación.
    /// </summary>
    internal static class EstiloApp
    {
        // ── Paleta de colores ─────────────────────────────────────
        internal static readonly Color Primario   = Color.FromArgb(26, 43, 74);    // Encabezados
        internal static readonly Color Secundario = Color.FromArgb(30, 58, 95);    // Submenús
        internal static readonly Color Azul       = Color.FromArgb(41, 128, 185);  // Acción principal
        internal static readonly Color Verde      = Color.FromArgb(39, 174, 96);   // Guardar / Confirmar
        internal static readonly Color Rojo       = Color.FromArgb(192, 57, 43);   // Eliminar / Peligro
        internal static readonly Color Gris       = Color.FromArgb(108, 117, 125); // Acción secundaria
        internal static readonly Color Fondo      = Color.FromArgb(245, 247, 250); // Fondo de formularios
        internal static readonly Color Texto      = Color.FromArgb(55, 55, 55);    // Texto general

        // ── Fuentes ───────────────────────────────────────────────
        internal static readonly Font FuenteNormal = new Font("Segoe UI", 9F);
        internal static readonly Font FuenteBoton  = new Font("Segoe UI", 9F, FontStyle.Bold);
        internal static readonly Font FuenteTitulo = new Font("Segoe UI", 16F, FontStyle.Bold);

        // ─────────────────────────────────────────────────────────
        /// <summary>Aplica el diseño completo a un formulario y sus controles.</summary>
        internal static void AplicarEstilo(Form form)
        {
            form.BackColor = Fondo;
            form.Font      = FuenteNormal;
            Recorrer(form.Controls);
            AplicarMenuStrip(form);
        }

        // ── Recorrido recursivo de controles ──────────────────────

        private static void Recorrer(Control.ControlCollection controles)
        {
            foreach (Control ctrl in controles)
            {
                if (ctrl is Button btn)
                    EstiloBoton(btn);
                else if (ctrl is DataGridView dgv)
                    EstiloGrilla(dgv);
                else if (ctrl is TextBox txt)
                    EstiloTextBox(txt);
                else if (ctrl is ComboBox cbo)
                    EstiloComboBox(cbo);
                else if (ctrl is Panel pnl && EsHeaderPanel(pnl))
                    EstiloHeaderPanel(pnl);
                else if (ctrl is Panel pnlN)
                    EstiloPanelNormal(pnlN);
                else if (ctrl is Label lbl)
                    EstiloLabel(lbl);
                else if (ctrl is NumericUpDown nud)
                    EstiloNumeric(nud);

                // Recursión — no entrar en DataGridView (gestiona sus propios hijos)
                if (ctrl.HasChildren && !(ctrl is DataGridView))
                    Recorrer(ctrl.Controls);
            }
        }

        // ── Estilos por tipo de control ───────────────────────────

        private static void EstiloBoton(Button btn)
        {
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Font      = FuenteBoton;
            btn.ForeColor = Color.White;
            btn.Cursor    = Cursors.Hand;

            Color fondo = ColorBoton(btn.Text);
            btn.BackColor = fondo;
            btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(
                Math.Min(255, fondo.R + 25),
                Math.Min(255, fondo.G + 25),
                Math.Min(255, fondo.B + 25));
        }

        private static Color ColorBoton(string texto)
        {
            string t = texto.ToUpperInvariant();
            if (t.Contains("ELIM") || t.Contains("QUITAR") || t.Contains("DESACTIV"))
                return Rojo;
            if (t.Contains("SALIR") || t.Contains("CANCEL") || t.Contains("CERRAR")
             || t.Contains("REFRES") || t.Contains("LIMPIAR"))
                return Gris;
            if (t.Contains("GUARD") || t.Contains("ACTUAL") || t.Contains("INGRES") || t.Contains("APLICAR"))
                return Verde;
            // Nuevo, Editar, Registrar, Activar, Agregar, Buscar …
            return Azul;
        }

        private static void EstiloGrilla(DataGridView dgv)
        {
            dgv.EnableHeadersVisualStyles = false;

            // Encabezado
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Primario;
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font      = FuenteBoton;
            dgv.ColumnHeadersDefaultCellStyle.Padding   = new Padding(5, 0, 0, 0);
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            try { dgv.ColumnHeadersHeight = 38; } catch { /* modo AutoSize activo */ }

            // Celdas
            dgv.DefaultCellStyle.SelectionBackColor      = Azul;
            dgv.DefaultCellStyle.SelectionForeColor      = Color.White;
            dgv.DefaultCellStyle.Font                    = FuenteNormal;
            dgv.DefaultCellStyle.Padding                 = new Padding(4, 0, 0, 0);
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 245, 252);

            // Grid general
            dgv.BackgroundColor = Color.White;
            dgv.BorderStyle     = BorderStyle.None;
            dgv.GridColor       = Color.FromArgb(220, 228, 235);
            dgv.RowTemplate.Height = 32;
        }

        private static void EstiloTextBox(TextBox txt)
        {
            txt.Font        = new Font("Segoe UI", 9.5F);
            txt.BorderStyle = BorderStyle.FixedSingle;
            txt.BackColor   = Color.White;
        }

        private static void EstiloComboBox(ComboBox cbo)
        {
            cbo.Font      = new Font("Segoe UI", 9.5F);
            cbo.FlatStyle = FlatStyle.Flat;
            cbo.BackColor = Color.White;
        }

        private static void EstiloNumeric(NumericUpDown nud)
        {
            nud.Font        = new Font("Segoe UI", 9.5F);
            nud.BorderStyle = BorderStyle.FixedSingle;
        }

        private static bool EsHeaderPanel(Panel pnl)
        {
            // Un panel es "header" si está anclado arriba y tiene un Label con fuente grande
            if (pnl.Dock != DockStyle.Top) return false;
            foreach (Control c in pnl.Controls)
                if (c is Label lbl && lbl.Font != null && lbl.Font.Size >= 14F)
                    return true;
            return false;
        }

        private static void EstiloHeaderPanel(Panel pnl)
        {
            pnl.BackColor = Primario;
            pnl.Height    = Math.Max(pnl.Height, 60);
            foreach (Control c in pnl.Controls)
            {
                if (c is Label lbl)
                {
                    lbl.ForeColor = Color.White;
                    if (lbl.Font.Size >= 14F)
                    {
                        lbl.Font     = FuenteTitulo;
                        lbl.Location = new Point(lbl.Location.X,
                                                 (pnl.Height - lbl.PreferredHeight) / 2);
                    }
                }
            }
        }

        private static void EstiloPanelNormal(Panel pnl)
        {
            if (pnl.BackColor == SystemColors.Control
             || pnl.BackColor == SystemColors.ControlDark
             || pnl.BackColor == SystemColors.Window
             || pnl.BackColor == Color.Empty)
                pnl.BackColor = Fondo;
        }

        private static void EstiloLabel(Label lbl)
        {
            if (lbl.ForeColor == SystemColors.ControlText
             || lbl.ForeColor == Color.Black)
                lbl.ForeColor = Texto;
        }

        // ── MenuStrip ─────────────────────────────────────────────

        private static void AplicarMenuStrip(Form form)
        {
            foreach (Control c in form.Controls)
            {
                if (!(c is MenuStrip menu)) continue;
                menu.BackColor = Primario;
                menu.ForeColor = Color.White;
                menu.Font      = FuenteNormal;
                menu.Renderer  = new ToolStripProfessionalRenderer(new MenuColorTable());
                foreach (ToolStripItem item in menu.Items)
                    EstiloMenuItem(item);
            }
        }

        private static void EstiloMenuItem(ToolStripItem item)
        {
            item.ForeColor = Color.White;
            item.BackColor = Primario;
            item.Font      = FuenteNormal;
            if (!(item is ToolStripMenuItem mItem)) return;
            foreach (ToolStripItem sub in mItem.DropDownItems)
            {
                sub.ForeColor = Color.White;
                sub.BackColor = Secundario;
                sub.Font      = FuenteNormal;
                if (sub is ToolStripMenuItem subMenu)
                    EstiloMenuItem(subMenu);
            }
        }

        // ── Tabla de colores para el MenuStrip ────────────────────

        private sealed class MenuColorTable : ProfessionalColorTable
        {
            public override Color MenuItemSelected               => Azul;
            public override Color MenuItemBorder                 => Azul;
            public override Color MenuBorder                     => Primario;
            public override Color ToolStripDropDownBackground    => Secundario;
            public override Color ImageMarginGradientBegin       => Secundario;
            public override Color ImageMarginGradientMiddle      => Secundario;
            public override Color ImageMarginGradientEnd         => Secundario;
            public override Color MenuItemSelectedGradientBegin  => Azul;
            public override Color MenuItemSelectedGradientEnd    => Azul;
            public override Color MenuItemPressedGradientBegin   => Verde;
            public override Color MenuItemPressedGradientEnd     => Verde;
            public override Color SeparatorDark                  => Color.FromArgb(50, 75, 110);
        }
    }
}
