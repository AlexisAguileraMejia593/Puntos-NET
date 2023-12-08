using Microsoft.Data.SqlClient;
using System.Data;

namespace BL
{
	public class Usuario
	{
		public static ML.Result Add(ML.Usuario usuario)
		{
			ML.Result result = new ML.Result();
			try
			{
				using (SqlConnection context = new SqlConnection(DL.Conexion.Get()))
				{
					string query = "UsuariosAdd";
					SqlCommand cmd = new SqlCommand(query, context);
					cmd.CommandType = System.Data.CommandType.StoredProcedure;
					SqlParameter[] collection = new SqlParameter[5];
					{
						collection[0] = new SqlParameter("@Nombre", SqlDbType.VarChar);
						collection[0].Value = usuario.Nombre;
						collection[1] = new SqlParameter("@ApellidoPaterno", SqlDbType.VarChar);
						collection[1].Value = usuario.ApellidoPaterno;
						collection[2] = new SqlParameter("@ApellidoMaterno", SqlDbType.VarChar);
						collection[2].Value = usuario.ApellidoMaterno;
						collection[3] = new SqlParameter("@FechaDeNacimiento", SqlDbType.DateTime);
						collection[3].Value = usuario.FechaNacimiento;
						collection[4] = new SqlParameter("@UserName", SqlDbType.VarChar);
						collection[4].Value = usuario.UserName;

						cmd.Parameters.AddRange(collection);
						cmd.Connection.Open();
						var id = cmd.ExecuteScalar();
						if (id != null)
						{
							result.Correct = true;
							result.Object = id;
						}
						else
						{
							result.Correct = false;
						}
					}
				}
			}
			catch (Exception e)
			{
				result.ErrorMessage = e.Message;
			}
			return result;
		}
		public static ML.Result GetByEmail(string username)
		{
			ML.Result result = new ML.Result();
			try
			{
				using (SqlConnection context = new SqlConnection(DL.Conexion.Get()))
				{
					string query = "UsuariosGetByEmail";
					SqlCommand cmd = new SqlCommand(query, context);
					cmd.CommandType = CommandType.StoredProcedure;

					SqlParameter[] collection = new SqlParameter[1];
					collection[0] = new SqlParameter("@UserName", SqlDbType.VarChar);
					collection[0].Value = username;

					cmd.Parameters.AddRange(collection);
					SqlDataAdapter adapter = new SqlDataAdapter(cmd);

					DataTable tableusuario = new DataTable();

					adapter.Fill(tableusuario);

					if (tableusuario.Rows.Count > 0)
					{
						DataRow row = tableusuario.Rows[0];

						ML.Usuario usuarioresult = new ML.Usuario();
						usuarioresult.IdUsuario = int.Parse(row[0].ToString());
						usuarioresult.Nombre = row[1].ToString();
						usuarioresult.ApellidoPaterno = row[2].ToString();
						usuarioresult.ApellidoMaterno = row[3].ToString();
						usuarioresult.FechaNacimiento = DateTime.Parse(row[4].ToString());
						usuarioresult.UserName = row[5].ToString();
						usuarioresult.Password = row[6].ToString();

						result.Object = usuarioresult;

						result.Correct = true;
					}

				}
			} catch (Exception ex)
			{
				result.ErrorMessage = ex.Message;
				result.Correct = false;
				result.Ex = ex;
			}
			return result;
		}
		public static ML.Result ChangePassword(int IdUsuario, string Password)
		{
			ML.Result result = new ML.Result();
			try
			{
				using (SqlConnection context = new SqlConnection(DL.Conexion.Get()))
				{
					string query = "ChangePassword";

					SqlCommand cmd = new SqlCommand(query, context);
					cmd.CommandType = CommandType.StoredProcedure;
					SqlParameter[] collection = new SqlParameter[2];

					collection[0] = new SqlParameter("@IdUsuarios", SqlDbType.VarChar);
					collection[0].Value = IdUsuario;
					collection[1] = new SqlParameter("@Password", SqlDbType.VarChar);
					collection[1].Value = Password;

					cmd.Parameters.AddRange(collection);
					cmd.Connection.Open();
					int filasAfectadas = cmd.ExecuteNonQuery();
					if (filasAfectadas > 0)
					{
						result.Correct = true;
					}
					else
					{
						result.Correct = false;
					}
				}
			} catch (Exception ex)
			{
				result.ErrorMessage = ex.Message;
				result.Correct = false;
			}
			return result;
		}
		public static ML.Result Update(ML.Usuario usuario)
		{
			ML.Result result = new ML.Result();
			try
			{
				using (SqlConnection context = new SqlConnection(DL.Conexion.Get()))
				{
                    string query = "UsuariosUpdate";
                    SqlCommand cmd = new SqlCommand(query, context);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    SqlParameter[] collection = new SqlParameter[5];
                    {
                        collection[0] = new SqlParameter("@Nombre", SqlDbType.VarChar);
                        collection[0].Value = usuario.Nombre;
                        collection[1] = new SqlParameter("@ApellidoPaterno", SqlDbType.VarChar);
                        collection[1].Value = usuario.ApellidoPaterno;
                        collection[2] = new SqlParameter("@ApellidoMaterno", SqlDbType.VarChar);
                        collection[2].Value = usuario.ApellidoMaterno;
                        collection[3] = new SqlParameter("@FechaDeNacimiento", SqlDbType.DateTime);
                        collection[3].Value = usuario.FechaNacimiento;
                        collection[4] = new SqlParameter("@UserName", SqlDbType.VarChar);
                        collection[4].Value = usuario.UserName;

                        cmd.Parameters.AddRange(collection);
                        cmd.Connection.Open();
                        int filasAfectadas = cmd.ExecuteNonQuery();
                        if (filasAfectadas > 0)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                        }
                    }
                }
			} catch (Exception ex)
			{
				result.ErrorMessage = ex.Message;
				result.Correct = false;
			}
			return result;
		}
        public static ML.Result Delete(int IdUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.Get()))
                {
                    string query = "UsuariosDelete";

                    SqlCommand cmd = new SqlCommand(query, context);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter[] collection = new SqlParameter[1];
                    collection[0] = new SqlParameter("@IdUsuario", SqlDbType.Int);
                    collection[0].Value = IdUsuario;

                    cmd.Parameters.AddRange(collection);

                    context.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    context.Close();

                    if (rowsAffected > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No rows were affected by the delete operation.";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
        public static ML.Result GetById(int IdUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.Get()))
                {
                    string query = "UsuariosGetById";

                    SqlCommand cmd = new SqlCommand(query, context);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter[] collection = new SqlParameter[1];
                    collection[0] = new SqlParameter("@IdUsuarios", SqlDbType.Int);
                    collection[0].Value = IdUsuario;

                    cmd.Parameters.AddRange(collection);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                    DataTable tablaUsurio = new DataTable();

                    adapter.Fill(tablaUsurio);

                    if (tablaUsurio.Rows.Count > 0)
                    {
                        DataRow row = tablaUsurio.Rows[0];

                        ML.Usuario usuarioResult = new ML.Usuario();
                        usuarioResult.IdUsuario = int.Parse(row[0].ToString());
                        usuarioResult.Nombre = row[1].ToString();
                        usuarioResult.ApellidoPaterno = row[2].ToString();
                        usuarioResult.ApellidoMaterno = row[3].ToString();
                        usuarioResult.FechaNacimiento = DateTime.Parse(row[4].ToString());
						usuarioResult.UserName = row[5].ToString();
						usuarioResult.Password = row[6].ToString();

                        //boxing
                        result.Object = usuarioResult;

                        result.Correct = true;

                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.Get()))
                {
                    string query = "UsuariosGetAll";

                    SqlCommand cmd = new SqlCommand(query, context);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                    DataTable tablaUsurio = new DataTable();

                    adapter.Fill(tablaUsurio);

                    if (tablaUsurio.Rows.Count > 0)
                    {
                        result.Objects = new List<object>();
                        foreach (DataRow row in tablaUsurio.Rows)
                        {
                            ML.Usuario usuario = new ML.Usuario();
                            usuario.IdUsuario = int.Parse(row[0].ToString());
                            usuario.Nombre = row[1].ToString();
                            usuario.ApellidoPaterno = row[2].ToString();
                            usuario.ApellidoMaterno = row[3].ToString();
                            usuario.FechaNacimiento = DateTime.Parse(row[4].ToString());
                            usuario.UserName = row[5].ToString();
                            usuario.Password = row[6].ToString();

                            result.Objects.Add(usuario);

                        }
                        result.Correct = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
    }
}