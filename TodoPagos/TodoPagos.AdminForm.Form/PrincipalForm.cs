using Domain;
using System;
using System.Windows.Forms;
using TodoPagos.AdminForm.Logic;
using TodoPagos.Domain.DataAccess;
using TodoPagos.Domain.Repository;
using TodoPagos.UserAPI;

namespace TodoPagos.AdminForm.Form
{
    public partial class PrincipalForm : System.Windows.Forms.Form
    {
        private LoginFacade todoPagosFacade;
        private ILogStrategy logStrategy;
        private IUnitOfWork unitOfWork;

        [STAThread]
        private static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(true);
            Application.Run(new PrincipalForm());
        }
        public PrincipalForm()
        {
            InitializeComponent();
            TodoPagosContext context = new TodoPagosContext();
            unitOfWork = new UnitOfWork(context);
            logStrategy = new LogDatabaseConcreteStrategy(unitOfWork);
            todoPagosFacade = new LoginFacade(unitOfWork);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                User loggedInUser = CheckForValidUser();
                AddEntryToLog();
                ChangeActivePanel(new PrincipalUserControl(logStrategy, unitOfWork, loggedInUser.Name, loggedInUser.Email));
            }
            catch (UnauthorizedAccessException)
            {
                ShowNoAuthorizationError();
            }
            catch (ArgumentException)
            {
                ShowEmailOrPasswordError();
            }
        }

        private void AddEntryToLog()
        {
            LogEntry newEntry = new LogEntry(ActionType.LOGIN, this.txtEmailLogin.Text);
            logStrategy.SaveEntry(newEntry);
        }

        private void ShowNoAuthorizationError()
        {
            ShowErrorMessage("Para poder iniciar sesión debe contar con privilegios de Administrador", "Error");
        }

        private void ShowEmailOrPasswordError()
        {
            ShowErrorMessage("Email o contraseña incorrectos, por favor verifique e intente nuevamente", "Error");
        }

        public void ShowErrorMessage(string description, string title)
        {
            MessageBox.Show(description, title, MessageBoxButtons.OK
                , MessageBoxIcon.Error);
        }

        private User CheckForValidUser()
        {
            string email = this.txtEmailLogin.Text;
            string password = this.txtPasswordLogin.Text;
            return todoPagosFacade.AdminLogin(email, password);
        }

        public void ChangeActivePanel(UserControl userControl)
        {
            activePanel.Visible = false;
            activePanel.Controls.Clear();
            activePanel.Controls.Add(userControl);
            activePanel.Visible = true;
        }
    }
}
