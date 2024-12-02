using System.Data;
using SQLite;


namespace VillalbaJuanJose
{
    public partial class MainPage : ContentPage
    {
        private string ultimaRecarga;
        public string UltimaRecarga
        {
            get => ultimaRecarga;
            set
            {
                ultimaRecarga = value;
                OnPropertyChanged(nameof(UltimaRecarga));
            }
        }
        private SQLiteConnection database;

        public MainPage()
        {
            InitializeComponent();
            BindingContext = this;
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "regargas.db");
            database = new SQLiteConnection(dbPath);
            database.CreateTablet<Recarga>();
            CargarUltimaRecarga();
        }
        private void OnRecargarClicked(object sender, EventArgs e)
        {
            string nombre = vjj_entrynombre.Text;
            string numero = vjj_entrytelefono.Text;
            if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(numero))
            {
                DisplayAlert("Error", "Por favor, ingresa todos los datos", "OK");
                return;
            }
            var Recarga = new Recarga
            {

            }
            string nombreArchivo = $"{nombre.Replace("", "")}.txt";
            string rutaArchivo = Path.Combine(FileSystem.AppDataDirectory, nombreArchivo);
            string contenido = $"Nombre: {nombre}\nNumero:{numero}\nFecha:{DateTime.Now}";
            File.WriteAllText(rutaArchivo, contenido);
            DisplayAlert("Exito", "La recarga fue exitosa", "OK");

            UltimaRecarga=contenido;
            vjj_entrynombre.Text = string.Empty;
            vjj_entrytelefono.Text = string.Empty;
        }
        private void CargarUltimaRecarga()
        {
            try
            {
                string[] archivos = Directory.GetFiles(FileSystem.AppDataDirectory, "*.txt");
                if (archivos.Length > 0)
                {
                    string ultimoArchivo = archivos[^1];
                    UltimaRecarga = File.ReadAllText(ultimoArchivo);
                }
                else
                {
                    UltimaRecarga = "No hay recargas registradas";
                }
            }
            catch (Exception ex) 
            {
                UltimaRecarga = $"Error al cargar recarga: {ex.Message}";
            }
        }


    }
}
