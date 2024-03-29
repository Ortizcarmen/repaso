﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace repaso
{
    public partial class Form1 : Form
    {
        List<Empleado> empleados = new List<Empleado>();
        List<Asistencia> asistencias = new List<Asistencia>();
        List<Reporte> reportes = new List<Reporte>();
        public Form1()
        {
            InitializeComponent();
        }
        
        public void CargarEmpleados()
        {
            //Leer el archivo y cargarlo a la lista
            string fileName = "Empleados.txt";

            FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(stream);

            while (reader.Peek() > -1)
            {
                //Leer los datos de un empleado
                Empleado empleado = new Empleado();
                empleado.NoEmpleado = Convert.ToInt16(reader.ReadLine());
                empleado.Nombre = reader.ReadLine();
                empleado.SueldoHora = Convert.ToDecimal(reader.ReadLine());

                //Guardar el empleado a la lista de empleados
                empleados.Add(empleado);
            }
            reader.Close();
        }

        public void MostrarEmpleados()
        {
            //Mostrar la lista de empleados en el Gridview
            dataGridViewEmpleados.DataSource = null;
            dataGridViewEmpleados.DataSource = empleados;
            dataGridViewEmpleados.Refresh();
        }

        public void CargarAsistencia()
        {
            //Leer el archivo y cargarlo a la lista
            string fileName = "Asistencias.txt";

            FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(stream);

            while (reader.Peek() > -1)
            {
                //Leer los datos de un empleado
                Asistencia asistencia = new Asistencia();
                asistencia.NoEmpleado = Convert.ToInt16(reader.ReadLine());
                asistencia.HorasMes = Convert.ToInt16(reader.ReadLine());
                asistencia.Mes = Convert.ToInt16(reader.ReadLine());

                asistencias.Add(asistencia);
            }
            reader.Close();
        }

        public void MostrarAsistencia()
        {
            //Mostrar la lista de empleados en el Gridview
            dataGridViewAsistencia.DataSource = null;
            dataGridViewAsistencia.DataSource = asistencias;
            dataGridViewAsistencia.Refresh();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            CargarEmpleados();
            CargarAsistencia();
            comboBoxEmpleados.DisplayMember = "Nombre";
            comboBoxEmpleados.ValueMember = "NoEmpleado";
            comboBoxEmpleados.DataSource = empleados;
        }

        private void buttonLeer_Click(object sender, EventArgs e)
        {
            //CargarEmpleados();
            MostrarEmpleados();
            CargarAsistencia();
            MostrarAsistencia();
        }

        private void buttonCalcular_Click(object sender, EventArgs e)
        {
            foreach (Empleado empleado in empleados)
            {
                foreach (Asistencia asistencia in asistencias)
                {
                    if (empleado.NoEmpleado == asistencia.NoEmpleado)
                    {
                        Reporte reporte = new Reporte();
                        reporte.NombreEmpleado = empleado.Nombre;
                        reporte.Mes = asistencia.Mes;
                        reporte.SueldoMensual = empleado.SueldoHora * asistencia.HorasMes;

                        reportes.Add(reporte);
                    }
                }
            }
            dataGridViewReporte.DataSource = null;
            dataGridViewReporte.DataSource = reportes;
            dataGridViewReporte.Refresh();
        }

        private void comboBoxEmpleados_SelectedIndexChanged(object sender, EventArgs e)
        {
            int noEmpleado = Convert.ToInt16(comboBoxEmpleados.SelectedValue);
            //Buscar por medio de Find
            Empleado empleadoEncontrado = empleados.Find(c => c.NoEmpleado == noEmpleado);
            Asistencia asistenciaEncontrada = asistencias.Find(c => c.NoEmpleado == noEmpleado);

            decimal sueldo = empleadoEncontrado.SueldoHora * asistenciaEncontrada.HorasMes;

            label4.Text = empleadoEncontrado.Nombre;
            label5.Text = sueldo.ToString();
        }
    }
}
