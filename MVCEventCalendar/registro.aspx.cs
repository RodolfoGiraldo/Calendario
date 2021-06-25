using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MVCEventCalendar
{
    public partial class registro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblError.Text = "";
            lblErrorPassword.Text = "";
            LeerDatos();

        }

        SqlConnection conexion = new SqlConnection("data source=DESKTOP-MIJ8A0U;initial catalog=Calendar;user id=sa;password=Juanma22;multipleactiveresultsets=True;application name=EntityFramework");

        void Limpiar()
        {
            tbNombreCompleto.Text = "";
            tbDireccion.Text = "";
            tbFechaNacimiento.Text = "";
            tbCorreo.Text = "";
            tbConfirmarPassword.Text = "";
            tbPassword.Text = "";
            lblError.Text = "";
            lblErrorPassword.Text = "";
        }
        protected void BtnRegistrar_Click(Object sender, EventArgs e)
        {
            if (tbNombreCompleto.Text == "" || tbDireccion.Text == "" || tbFechaNacimiento.Text == "" || tbCorreo.Text == "" || tbConfirmarPassword.Text == "" || tbPassword.Text == "")
            {
                lblError.Text = "Todos los campos son obligatorios!";
            }
            else
            {
                if (tbPassword.Text != tbConfirmarPassword.Text)
                {
                    lblErrorPassword.Text = "Las contrasenias no coinciden!";
                }
                else
                {
                    conexion.Open();
                    SqlCommand cmd = new SqlCommand("select count(*) from Users where Correo='" + tbCorreo.Text + "'", conexion)
                    {
                        CommandType = System.Data.CommandType.Text
                    };
                    cmd.Parameters.AddWithValue("Correo", tbCorreo.Text);
                    int usuario = Convert.ToInt32(cmd.ExecuteScalar()); 
                     string patron = "arquesoft";
                    if (usuario < 1)
                    {
                        SqlCommand cmm = new SqlCommand("Insert into Users values('" + tbNombreCompleto.Text + "','" + tbCorreo.Text + "'," +
                            "'" + tbDireccion.Text + "','" + tbFechaNacimiento.Text + "',(EncryptByPassPhrase('" + patron + "','" + tbPassword.Text + "')))", conexion);
                        cmm.ExecuteNonQuery();
                        conexion.Close();
                        Limpiar();
                        LeerDatos();

                        Response.Redirect("/Home/Index");
                    }
                    else
                    {
                        lblError.Text = "El Usuario " + tbCorreo.Text + " ya existe!";
                        tbCorreo.Text = "";
                    }
                }
            }
            
        }
        void LeerDatos()
        {
            SqlCommand leerdatos = new SqlCommand("Select * from Users", conexion);
            SqlDataAdapter da = new SqlDataAdapter(leerdatos);
            DataTable dt = new DataTable();
            da.Fill(dt);
        }
    }
}