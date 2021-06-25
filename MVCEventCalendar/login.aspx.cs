using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


namespace MVCEventCalendar
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["usuariologueado"] = null;
        }

        String patron = "arquesoft";
        protected void BtnIngresar_Click(object sender, EventArgs e)
        {
            string conectar = "data source=DESKTOP-MIJ8A0U;initial catalog=Calendar;persist security info=True;user id=sa;password=Juanma22;MultipleActiveResultSets=True;App=EntityFramework";
            SqlConnection sqlConectar = new SqlConnection(conectar);
            SqlCommand cmd = new SqlCommand("ValidarUsuario", sqlConectar)
            {
                CommandType = CommandType.StoredProcedure
            };
            cmd.Connection.Open();
            cmd.Parameters.Add("@correo", SqlDbType.VarChar, 50).Value = tbUsuario.Text;
            cmd.Parameters.Add("@Password", SqlDbType.VarChar, 50).Value = tbPassword.Text;
            cmd.Parameters.Add("@Patron", SqlDbType.VarChar, 50).Value = patron;
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {

                //Agregamos una sesion de usuario
                BuscarIdUsuario(tbUsuario.Text);
                Session["usuariologueado"] = tbUsuario.Text;
                Response.Redirect("/Home/Index");
            }
            else
            {
                lblError.Text = "Error de Usuario o Contraseña";
            }
            

            cmd.Connection.Close();
        }
        public  void BtnRegistrar_Click(object sender, EventArgs e)
        {
            Response.Redirect("/registro.aspx");
        }

        public void BuscarIdUsuario(string correoAux)
        {
            using (MyDatabaseEntities dc = new MyDatabaseEntities())
            {
                var user = dc.Users.Where(b => b.correo == correoAux).ToList();
                foreach (Users value in user)
                {
                    Session["idUsuario"] = value.id;
                }
            }
               
        }
    }
}