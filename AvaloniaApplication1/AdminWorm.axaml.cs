using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using AvaloniaApplication1.Script_C_;
using MySql.Data.MySqlClient;
using ClosedXML.Excel;

namespace AvaloniaApplication1;

public partial class AdminWorm : Window
{
    private List<Scripts> _script;
    private List<Svoystva_vesh> _svoystvaVeshes;
    private List<monitor> _monitors;
    private List<stream_vbr> _vbrs;
    private string _connString = "server=localhost;Database=metrology;UserID=root;password=Ghost45)";
    private MySqlConnection _sqlConnection;
    private List<filter_Meteo> _filterMeteos;

    public AdminWorm()
    {
        InitializeComponent();
        string sql = "SELECT * FROM meteo_climat";
        string sql2 = "SELECT * FROM svo_vesh";
        string sql3 = "SELECT * FROM monitoring";
        string sql4 = "SELECT * FROM stream_vbros";
        Tables1(sql);
        Tables2(sql2);
        Tables3(sql3);
        Tables4(sql4);
        FilterUser();
    }
    
    
     private void Tables1(string sql)
    {
        _script = new List<Scripts>();
        _sqlConnection = new MySqlConnection(_connString);
        _sqlConnection.Open();
        MySqlCommand command = new MySqlCommand(sql, _sqlConnection);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var currentmeteo = new Scripts()
            {
                id = reader.GetInt32("id"),
                stancia = reader.GetInt32("stan_con"),
                data_vre = reader.GetDateTime("data_vrem"),
                temperature = reader.GetString("temper"),
                vlash_H2 = reader.GetString("vlazh_v"),
                sp_vetra = reader.GetString("sp_veter"),
                napravlenie = reader.GetString("napravlenie"),
                atmos_dav = reader.GetString("atmos_davle"),
                oblach = reader.GetString("oblach"),
                nalichie_osad = reader.GetString("nal_osad")
            };
            _script.Add(currentmeteo);
        }
        _sqlConnection.Close();
        Grid1.ItemsSource = _script;
    }
    
    private void Tables2(string sql)
    {
        _svoystvaVeshes = new List<Svoystva_vesh>();
        _sqlConnection = new MySqlConnection(_connString);
        _sqlConnection.Open();
        MySqlCommand command = new MySqlCommand(sql, _sqlConnection);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var current = new Svoystva_vesh()
            {
                id1 = reader.GetInt32("id"),
                zagryz_ve = reader.GetInt32("zagryz_vesh"),
                average_sutoch = reader.GetString("sred_sut"),
                pdk_m = reader.GetInt32("pdk_m_r"),
                pdk_rab= reader.GetInt32("pdk_v_rab"),
                pdk_poch = reader.GetString("pdk_poch"),
                cl_opasnos = reader.GetInt32("class_dang"),
                plots= reader.GetString("plot"),
                temperature= reader.GetString("temper")
            };
            _svoystvaVeshes.Add(current);
        }
        _sqlConnection.Close();
        Grid2.ItemsSource = _svoystvaVeshes;
    }
    
    private void Tables3(string sql)
    {
        _monitors = new List<monitor>();
        _sqlConnection = new MySqlConnection(_connString);
        _sqlConnection.Open();
        MySqlCommand command = new MySqlCommand(sql, _sqlConnection);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var current = new monitor()
            {
                id2 = reader.GetInt32("id"),
                stanc = reader.GetInt32("stancia"),
                gryaz = reader.GetInt32("zagryaz"),
                data_pr = reader.GetDateTime("data_prob"),
                value_con = reader.GetInt32("value_conc")
            };
            _monitors.Add(current);
        }
        _sqlConnection.Close();
        Grid3.ItemsSource = _monitors;
    }
    
    private void Tables4(string sql)
    {
        _vbrs = new List<stream_vbr>();
        _sqlConnection = new MySqlConnection(_connString);
        _sqlConnection.Open();
        MySqlCommand command = new MySqlCommand(sql, _sqlConnection);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var current = new stream_vbr()
            {
                id3 = reader.GetInt32("id"),
                name_st = reader.GetString("name_str"),
                type_st = reader.GetString("type_str"),
                zagryaz_d = reader.GetInt32("zagryz_id"),
                ustanov = reader.GetInt32("ustanovka"),
                techno_proc = reader.GetInt32("tehno_process"),
                valov_mos = reader.GetString("valov_mosh"),
                temp_v = reader.GetInt32("temper_v"),
                sp_v = reader.GetInt32("speed_v"),
                ob_ras = reader.GetString("ob_rash"),
                dolya_ves = reader.GetString("dolya_vesh")
            };
            _vbrs.Add(current);
        }
        _sqlConnection.Close();
        Grid5.ItemsSource = _vbrs;
    }


    private void AddOnClick(object? sender, RoutedEventArgs e)
    {
        try
        {
            _sqlConnection.Open();
            MySqlCommand command =
                new MySqlCommand(
                    $"Insert into meteo_climat  (stan_con, data_vrem, temper, vlazh_v, sp_veter, napravlenie, atmos_davle, oblach, nal_osad) Values ('"+t1.Text+"', '"+t2.Text+"', '"+t3.Text+"', '"+t4.Text+"', '"+t5.Text+"', '"+t6.Text+"', '"+t7.Text+"', '"+t8.Text+"', '"+t9.Text+"')", _sqlConnection);

            command.ExecuteNonQuery();
            _sqlConnection.Close();
        }
        catch (Exception exception)
        {
            Debug.WriteLine("Эта запись используется в других таблицах", ID_TextBox.Text = exception.Message);
        }
    }

    private void SaveOnClick(object? sender, RoutedEventArgs e)
    {
        _script = new List<Scripts>();
        string sql = "SELECT * FROM meteo_climat";
        _sqlConnection = new MySqlConnection(_connString);
        _sqlConnection.Open();
        MySqlCommand command = new MySqlCommand(sql, _sqlConnection);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var currentmeteo = new Scripts()
            {
                id = reader.GetInt32("id"),
                stancia = reader.GetInt32("stan_con"),
                data_vre = reader.GetDateTime("data_vrem"),
                temperature = reader.GetString("temper"),
                vlash_H2 = reader.GetString("vlazh_v"),
                sp_vetra = reader.GetString("sp_veter"),
                napravlenie = reader.GetString("napravlenie"),
                atmos_dav = reader.GetString("atmos_davle"),
                oblach = reader.GetString("oblach"),
                nalichie_osad = reader.GetString("nal_osad")
            };
            _script.Add(currentmeteo);
        }
        _sqlConnection.Close();
        Grid1.ItemsSource = _script;
    }

    private void DeleteOnClick(object? sender, RoutedEventArgs e)
    {
        try
        {
            _sqlConnection.Open();
            string QeuryString = $"delete from meteo_climat where ID = {ID_TextBox.Text}";
            MySqlCommand command = new MySqlCommand(QeuryString, _sqlConnection);
            command.ExecuteNonQuery();
            _sqlConnection.Close();
        }
        catch (Exception)
        {

            Debug.WriteLine("Эта запись используется в других таблицах", ID_TextBox.Text);
        }
    }

    private void UpdateOnClick(object? sender, RoutedEventArgs e)
    {
        try
        {
            _sqlConnection.Open();
            string QueryString = $"update meteo_climat set stan_con = '"+t1.Text+"', data_vrem = '"+t2.Text+"', temper = '"+t3.Text+"', vlazh_v = '"+t4.Text+"', sp_veter = '"+t5.Text+"', napravlenie = '"+t6.Text+"', atmos_davle = '"+t7.Text+"', oblach = '"+t8.Text+"', nal_osad = '"+t9.Text+"' where ID = '"+ID_TextBox.Text+"'";
            MySqlCommand command = new MySqlCommand(QueryString, _sqlConnection);
            command.ExecuteNonQuery();
            _sqlConnection.Close();
        }
        catch (Exception)
        {

            Debug.WriteLine("Эта запись используется в других таблицах", ID_TextBox.Text);
        }
    }

    private void ExcelOnClick(object? sender, RoutedEventArgs e)
    {
        string FileName = "sa";
        
        var workbook = new XLWorkbook();
        var worksheet = workbook.Worksheets.Add("Лист1");
        worksheet.Cell("A" + 1).Value = "Column1";
        worksheet.Cell("B" + 1).Value = "Column2";
        worksheet.Cell("C" + 1).Value = "Column3";
        worksheet.Cell("D" + 1).Value = "Column4";
        worksheet.Cell("E" + 1).Value = "Column5";
        worksheet.Cell("F" + 1).Value = "Column6";
        worksheet.Cell("G" + 1).Value = "Column7";
        worksheet.Cell("H" + 1).Value = "Column8";
        worksheet.Cell("I" + 1).Value = "Column9";
        worksheet.Cell("J" + 1).Value = "Column10";
        int row = 2;
        
        foreach (Scripts data in _script)
        {
            worksheet.Cell("A" + row).Value = data.id;
            worksheet.Cell("B" + row).Value = data.stancia;
            worksheet.Cell("C" + row).Value = data.data_vre;
            worksheet.Cell("D" + row).Value = data.temperature;
            worksheet.Cell("E" + row).Value = data.vlash_H2;
            worksheet.Cell("F" + row).Value = data.sp_vetra;
            worksheet.Cell("G" + row).Value = data.napravlenie;
            worksheet.Cell("H" + row).Value = data.atmos_dav;
            worksheet.Cell("I" + row).Value = data.oblach;
            worksheet.Cell("J" + row).Value = data.nalichie_osad;
            row++;
        }
 
        worksheet.Columns().AdjustToContents(); 
 
        workbook.SaveAs(@".\Output\" + FileName + ".xlsx");
    }
    
    
    private void FilterUser()
    {
        _filterMeteos = new List<filter_Meteo>();
        _sqlConnection = new MySqlConnection(_connString);
        _sqlConnection.Open();
        MySqlCommand command = new MySqlCommand("SELECT id, napravlenie FROM meteo_climat", _sqlConnection);
        MySqlDataReader reader = command.ExecuteReader();
        while (reader.Read() && reader.HasRows)
        {
            var current = new filter_Meteo()
            {
                id4 = reader.GetInt32("id"),
                napravl = reader.GetString("napravlenie")
            };
            _filterMeteos.Add(current);
        }
        _sqlConnection.Close();
        var comboBox = this.FindControl<ComboBox>("Box");
        comboBox.ItemsSource = _filterMeteos;
    }
    private void Search_OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        string sqlsearch = "select * from meteo_climat WHERE temper LIKE '%"+Search.Text+"%' OR napravlenie LIKE '%"+Search.Text+"%'";
        Tables1(sqlsearch);
    }

    private void Box_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        var comboBox = (ComboBox)sender;
        var current = comboBox.SelectedItem as filter_Meteo;
        var filter = _script
            .Where(x => x.id == current.id4)
            .ToList();
        Grid1.ItemsSource = filter;
    }

    private void Button_OnClick(object? sender, RoutedEventArgs e)
    {
        MainWindow mainWindow = new MainWindow();
        mainWindow.Show();
        this.Close();
    }

    private void Button_Filter(object? sender, RoutedEventArgs e)
    {
        string sqlFilter = "select * from meteo_climat WHERE temper LIKE '+12' OR napravlenie LIKE 'Sever'";
        Tables1(sqlFilter);
    }
}