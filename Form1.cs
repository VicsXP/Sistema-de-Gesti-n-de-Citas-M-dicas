using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema_de_Gestión_de_Citas_Médicas
{
    public partial class Form1 : Form
    {
        List<Doctor> doctores = new List<Doctor>();
        List<Paciente> pacientes = new List<Paciente>();
        List<Cita> citas = new List<Cita>();

        public Form1()
        {
            InitializeComponent();

            CargarDoctores();
            CargarPacientes();
            CargarCombos();
            CargarHoras();
            CargarCitasDesdeArchivo();
        }

        void CargarDoctores()
        {
            const string file = "doctores.txt";
            if (!File.Exists(file)) return;

            using (var reader = new StreamReader(file))
            {
                while (!reader.EndOfStream)
                {
                    string idLine = reader.ReadLine();
                    string nombre = reader.ReadLine();
                    string especialidad = reader.ReadLine();

                    if (string.IsNullOrWhiteSpace(idLine)) break;

                    int id;
                    if (!int.TryParse(idLine, out id)) continue;

                    var d = new Doctor(id, nombre ?? string.Empty, especialidad ?? string.Empty);
                    doctores.Add(d);
                }
            }
        }

        void CargarPacientes()
        {
            const string file = "pacientes.txt";
            if (!File.Exists(file)) return;

            using (var reader = new StreamReader(file))
            {
                while (!reader.EndOfStream)
                {
                    string nombre = reader.ReadLine();
                    string dpi = reader.ReadLine();
                    string telefono = reader.ReadLine();

                    if (string.IsNullOrWhiteSpace(nombre)) break;

                    if (dpi == null) dpi = string.Empty;
                    if (telefono == null) telefono = string.Empty;

                    var p = new Paciente(dpi.Trim(), nombre.Trim(), telefono.Trim());
                    pacientes.Add(p);
                }
            }
        }

        void CargarCombos()
        {
            cmbDoctor.DataSource = null;
            cmbDoctor.DataSource = doctores;
            cmbDoctor.DisplayMember = "Nombre";
            cmbDoctor.ValueMember = "ID";

            cmbPaciente.DataSource = null;
            cmbPaciente.DataSource = pacientes;
            cmbPaciente.DisplayMember = "NombreP";
            cmbPaciente.ValueMember = "DPI";
        }

        void CargarHoras()
        {
            comboBox3.Items.Clear();
            var start = new TimeSpan(8, 0, 0);
            var end = new TimeSpan(17, 30, 0);
            for (var t = start; t <= end; t = t.Add(TimeSpan.FromMinutes(30)))
            {
                comboBox3.Items.Add(t.ToString(@"hh\:mm"));
            }

            if (comboBox3.Items.Count > 0)
                comboBox3.SelectedIndex = 0;
        }

        void GuardarCita()
        {
            if (cmbDoctor.SelectedItem == null)
            {
                MessageBox.Show("Seleccione un doctor.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbPaciente.SelectedItem == null)
            {
                MessageBox.Show("Seleccione un paciente.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (comboBox3.SelectedItem == null)
            {
                MessageBox.Show("Seleccione una hora.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var c = new Cita
            {
                DoctorID = (int)cmbDoctor.SelectedValue,
                PacienteDPI = cmbPaciente.SelectedValue?.ToString() ?? string.Empty,
                Fecha = dateTimePicker1.Value.Date,
                Hora = comboBox3.SelectedItem.ToString()
            };

            citas.Add(c);
            GuardarCitasEnArchivo();

            MessageBox.Show("Cita agendada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        void GuardarCitasEnArchivo()
        {
            const string file = "citas.txt";
            using (var writer = new StreamWriter(file, false, Encoding.UTF8))
            {
                foreach (var c in citas)
                {
                    writer.WriteLine(c.DoctorID);
                    writer.WriteLine(c.PacienteDPI ?? string.Empty);
                    writer.WriteLine(c.Fecha.ToString("yyyy-MM-dd"));
                    writer.WriteLine(c.Hora ?? string.Empty);
                }
            }
        }

        void CargarCitasDesdeArchivo()
        {
            const string file = "citas.txt";
            if (!File.Exists(file)) return;

            using (var reader = new StreamReader(file))
            {
                while (!reader.EndOfStream)
                {
                    string idLine = reader.ReadLine();
                    string dpi = reader.ReadLine();
                    string fechaLine = reader.ReadLine();
                    string hora = reader.ReadLine();

                    if (string.IsNullOrWhiteSpace(idLine)) break;

                    int doctorId;
                    DateTime fecha;
                    if (!int.TryParse(idLine, out doctorId)) continue;
                    if (!DateTime.TryParse(fechaLine, out fecha)) fecha = DateTime.Today;

                    var c = new Cita
                    {
                        DoctorID = doctorId,
                        PacienteDPI = dpi ?? string.Empty,
                        Fecha = fecha.Date,
                        Hora = hora ?? string.Empty
                    };

                    citas.Add(c);
                }
            }
        }


        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void btnAgendar_Click(object sender, EventArgs e)
        {
            GuardarCita();
        }
    }
}
