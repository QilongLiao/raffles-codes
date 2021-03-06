using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Data.OracleClient;
using System.Reflection;

namespace DetailInfo
{
    public partial class SearchForm : Form
    {
        public SearchForm()
        {
            InitializeComponent();
            //this.skinEngine1.SkinFile = Application.StartupPath + "\\Resources\\DiamondBlue.ssk";
            this.skinEngine1.SkinFile = Application.StartupPath + "\\Resources\\" + User.skinstr;
        }
        public string conditionSql = "";
        private string connector = "";
        private string wheresql0 = "";
        string templatestr = "";
        string sqlfinal = "";
        private string table_name;
        public string Table_name
        {
            get { return table_name; }
            set { table_name = value; }
        }
        public DataSet dset = new DataSet();

        private string sql = string.Empty; 
        private void SearchForm_Load(object sender, EventArgs e)
        {
            int i = MDIForm.pMainWin.MdiChildren.Length;
            DataSet ds = new DataSet();
            if (table_name == "SP_SPOOL_TAB" && (MDIForm.pMainWin.ActiveMdiChild.Text == "设计小票概览" || MDIForm.pMainWin.ActiveMdiChild.Text == "加工小票概览" || MDIForm.pMainWin.ActiveMdiChild.Text == "质检小票概览" || MDIForm.pMainWin.ActiveMdiChild.Text == "安装小票概览" || MDIForm.pMainWin.ActiveMdiChild.Text == "调试小票概览"))
            {
                sql = @"select c.column_name,
       c.comments as 项目,
       (SELECT data_type
          FROM User_Tab_Cols u
         where table_name = upper('SP_SPOOL_TAB')
           and u.column_name = c.column_name) as 类型
  from user_col_comments c
  join user_tab_columns b
    ON (c.table_name = b.table_name and c.column_name = b.column_name)
 where c.table_name = upper('SP_SPOOL_TAB')
   and c.column_name not in ('ELBOWTYPE',
                             'WELDTYPE',
                             'FLOWSTATUSREMARK',
                             'LOCKED',
                             'ALLOCATIONTIME',
                             'SYSTEMTIME',
                             'REMARK',
                             'PAGE',
                             'MODIFYPAGE',
                             'XPOS',
                             'YPOS',
                             'ZPOS',
                             'DELETEMARK',
                             'DELETEPERSON',
                             'PREFABRICATETIME',
                             'WELDTIME',
                             'TOQCTIME',
                             'TRANSTIME',
                             'PRESSURETESTTIME')
 ORDER BY b.column_id";
            }

            else if (table_name == "SPL_VIEW")
            {
                sql = "select column_name,comments as 项目,(SELECT data_type FROM User_Tab_Cols u where table_name = upper('SPL_VIEW') and u.column_name = c.column_name) as 类型 from user_col_comments c where table_name = upper('SPL_VIEW') ";
            }

            else if (table_name == "MATERIALATTACHMENT_VIEW")
            {
                sql = "select column_name,comments as 项目,(SELECT data_type FROM User_Tab_Cols u where table_name = upper('MATERIALATTACHMENT_VIEW') and u.column_name = c.column_name) as 类型 from user_col_comments c where table_name = upper('MATERIALATTACHMENT_VIEW') ";
            }

            else if (table_name == "SP_VALVE_VIEW")
            {
                sql = @"select c.column_name,
       c.comments as 项目,
       (SELECT data_type
          FROM User_Tab_Cols u
         where table_name = upper('SP_VALVE_VIEW')
           and u.column_name = c.column_name) as 类型
  from user_col_comments c
  join user_tab_columns b
    ON (c.table_name = b.table_name and c.column_name = b.column_name)
 where c.table_name = upper('SP_VALVE_VIEW')
 ORDER BY b.column_id";
            } 

            else if (table_name == "SP_SPOOLMATERIAL_VIEW" && MDIForm.pMainWin.ActiveMdiChild.Text == "材料信息概览")
            {
                sql = @"select c.column_name,
       c.comments as 项目,
       (SELECT data_type
          FROM User_Tab_Cols u
         where table_name = upper('SP_SPOOLMATERIAL_VIEW')
           and u.column_name = c.column_name) as 类型
  from user_col_comments c
  join user_tab_columns b
    ON (c.table_name = b.table_name and c.column_name = b.column_name)
 where c.table_name = upper('SP_SPOOLMATERIAL_VIEW')
 ORDER BY b.column_id";
            }

            else if (table_name == "SP_CONNECTOR_VIEW")
            {
                sql = @"select c.column_name,
       c.comments as 项目,
       (SELECT data_type
          FROM User_Tab_Cols u
         where table_name = upper('SP_CONNECTOR_VIEW')
           and u.column_name = c.column_name) as 类型
  from user_col_comments c
  join user_tab_columns b
    ON (c.table_name = b.table_name and c.column_name = b.column_name)
 where c.table_name = upper('SP_CONNECTOR_VIEW')
   and c.column_name not in ('SYSTEMTIME', 'FLAG')
 ORDER BY b.column_id";
            }

            else if (table_name == "SP_MACHININGINFO_TAB")
            {
                sql = @"select c.column_name,
       c.comments as 项目,
       (SELECT data_type
          FROM User_Tab_Cols u
         where table_name = upper('SP_MACHININGINFO_TAB')
           and u.column_name = c.column_name) as 类型
  from user_col_comments c
  join user_tab_columns b
    ON (c.table_name = b.table_name and c.column_name = b.column_name)
 where c.table_name = upper('SP_MACHININGINFO_TAB')
   and c.column_name not in ('SYSTEMTIME', 'FLAG')
 ORDER BY b.column_id";
            }

            else if (table_name == "SP_SPOOLWELD_VIEW")
            {
                sql = @"select c.column_name,
       c.comments as 项目,
       (SELECT data_type
          FROM User_Tab_Cols u
         where table_name = upper('SP_SPOOLWELD_VIEW')
           and u.column_name = c.column_name) as 类型
  from user_col_comments c
  join user_tab_columns b
    ON (c.table_name = b.table_name and c.column_name = b.column_name)
 where c.table_name = upper('SP_SPOOLWELD_VIEW')
 ORDER BY b.column_id
";
            }

            else if (table_name == "SP_NORMALPIPEWORKINGHOUR_VIEW")
            {
                sql = @"select c.column_name,
       c.comments as 项目,
       (SELECT data_type
          FROM User_Tab_Cols u
         where table_name = upper('SP_NORMALPIPEWORKINGHOUR_VIEW')
           and u.column_name = c.column_name) as 类型
  from user_col_comments c
  join user_tab_columns b
    ON (c.table_name = b.table_name and c.column_name = b.column_name)
 where c.table_name = upper('SP_NORMALPIPEWORKINGHOUR_VIEW')
 ORDER BY b.column_id";
            }

            else if (table_name == "SP_MATERIALPREPARETIME_VIEW")
            {
                sql = @"select c.column_name,
       c.comments as 项目,
       (SELECT data_type
          FROM User_Tab_Cols u
         where table_name = upper('SP_MATERIALPREPARETIME_VIEW')
           and u.column_name = c.column_name) as 类型
  from user_col_comments c
  join user_tab_columns b
    ON (c.table_name = b.table_name and c.column_name = b.column_name)
 where c.table_name = upper('SP_MATERIALPREPARETIME_VIEW')
 ORDER BY b.column_id";
            }

            else if (table_name == "SP_MATERIALEQUIPRATION_TAB")
            {
                                sql = @"select c.column_name,
       c.comments as 项目,
       (SELECT data_type
          FROM User_Tab_Cols u
         where table_name = upper('SP_MATERIALEQUIPRATION_TAB')
           and u.column_name = c.column_name) as 类型
  from user_col_comments c
  join user_tab_columns b
    ON (c.table_name = b.table_name and c.column_name = b.column_name)
 where c.table_name = upper('SP_MATERIALEQUIPRATION_TAB')
and c.column_name not in ('FLAG')
 ORDER BY b.column_id";
            }

            #region 管加工车间普通碳钢管工时定额
            else if (table_name == "SP_SPOOL_TAB" && MDIForm.pMainWin.ActiveMdiChild.Text == "装配工时定额")
            {
                sql = @"select c.column_name,
       c.comments as 项目,
       (SELECT data_type
          FROM User_Tab_Cols u
         where table_name = upper('SP_SPOOL_TAB')
           and u.column_name = c.column_name) as 类型
  from user_col_comments c
  join user_tab_columns b
    ON (c.table_name = b.table_name and c.column_name = b.column_name)
 where c.table_name = upper('SP_SPOOL_TAB')
   and c.column_name  in ('PROJECTID',
                          'SPOOLNAME',
                          'SYSTEMID',
                          'SYSTEMNAME',
                          'SPOOLWEIGHT',
                          'DRAWINGNO',
                          'BLOCKNO',
                          'PREFABRICATETIME')
 ORDER BY b.column_id";
            }

            else if (table_name == "SP_SPOOL_TAB" && MDIForm.pMainWin.ActiveMdiChild.Text == "焊接工时定额")
            {
                sql = @"select c.column_name,
       c.comments as 项目,
       (SELECT data_type
          FROM User_Tab_Cols u
         where table_name = upper('SP_SPOOL_TAB')
           and u.column_name = c.column_name) as 类型
  from user_col_comments c
  join user_tab_columns b
    ON (c.table_name = b.table_name and c.column_name = b.column_name)
 where c.table_name = upper('SP_SPOOL_TAB')
   and c.column_name  in ('PROJECTID',
                          'SPOOLNAME',
                          'SYSTEMID',
                          'SYSTEMNAME',
                          'SPOOLWEIGHT',
                          'DRAWINGNO',
                          'BLOCKNO',
                          'WELDTIME')
 ORDER BY b.column_id";
            }

            else if (table_name == "SP_SPOOL_TAB" && MDIForm.pMainWin.ActiveMdiChild.Text == "报验工时定额")
            {
                sql = @"select c.column_name,
       c.comments as 项目,
       (SELECT data_type
          FROM User_Tab_Cols u
         where table_name = upper('SP_SPOOL_TAB')
           and u.column_name = c.column_name) as 类型
  from user_col_comments c
  join user_tab_columns b
    ON (c.table_name = b.table_name and c.column_name = b.column_name)
 where c.table_name = upper('SP_SPOOL_TAB')
   and c.column_name  in ('PROJECTID',
                          'SPOOLNAME',
                          'SYSTEMID',
                          'SYSTEMNAME',
                          'SPOOLWEIGHT',
                          'DRAWINGNO',
                          'BLOCKNO',
                          'TOQCTIME')
 ORDER BY b.column_id";
            }

            else if (table_name == "SP_SPOOL_TAB" && MDIForm.pMainWin.ActiveMdiChild.Text == "料场工时定额")
            {
                sql = @"select c.column_name,
       c.comments as 项目,
       (SELECT data_type
          FROM User_Tab_Cols u
         where table_name = upper('SP_SPOOL_TAB')
           and u.column_name = c.column_name) as 类型
  from user_col_comments c
  join user_tab_columns b
    ON (c.table_name = b.table_name and c.column_name = b.column_name)
 where c.table_name = upper('SP_SPOOL_TAB')
   and c.column_name  in ('PROJECTID',
                          'SPOOLNAME',
                          'SYSTEMID',
                          'SYSTEMNAME',
                          'SPOOLWEIGHT',
                          'DRAWINGNO',
                          'BLOCKNO',
                          'TRANSTIME')
 ORDER BY b.column_id";
            }

            else if (table_name == "SP_SPOOL_TAB" && MDIForm.pMainWin.ActiveMdiChild.Text == "压力试验工时定额")
            {
                sql = @"select c.column_name,
       c.comments as 项目,
       (SELECT data_type
          FROM User_Tab_Cols u
         where table_name = upper('SP_SPOOL_TAB')
           and u.column_name = c.column_name) as 类型
  from user_col_comments c
  join user_tab_columns b
    ON (c.table_name = b.table_name and c.column_name = b.column_name)
 where c.table_name = upper('SP_SPOOL_TAB')
   and c.column_name  in ('PROJECTID',
                          'SPOOLNAME',
                          'SYSTEMID',
                          'SYSTEMNAME',
                          'SPOOLWEIGHT',
                          'DRAWINGNO',
                          'BLOCKNO',
                          'PRESSURETESTTIME')
 ORDER BY b.column_id";
            }
            #endregion
            #region 管加工车间管理节点查询
            else if (table_name == "SP_WORKSHOPBLANKING_VIEW")
            {
                sql = @"select c.column_name,
       c.comments as 项目,
       (SELECT data_type
          FROM User_Tab_Cols u
         where table_name = upper('SP_WORKSHOPBLANKING_VIEW')
           and u.column_name = c.column_name) as 类型
  from user_col_comments c
  join user_tab_columns b
    ON (c.table_name = b.table_name and c.column_name = b.column_name)
 where c.table_name = upper('SP_WORKSHOPBLANKING_VIEW')
and c.column_name not in ('BLANKINGPERSON',
       'BLANKINGDATE') 
 ORDER BY b.column_id";
            }
            else if (table_name == "SP_WORKSHOPASSEMBLY_VIEW")
            {
                sql = @"select c.column_name,
       c.comments as 项目,
       (SELECT data_type
          FROM User_Tab_Cols u
         where table_name = upper('SP_WORKSHOPASSEMBLY_VIEW')
           and u.column_name = c.column_name) as 类型
  from user_col_comments c
  join user_tab_columns b
    ON (c.table_name = b.table_name and c.column_name = b.column_name)
 where c.table_name = upper('SP_WORKSHOPASSEMBLY_VIEW') 
and c.column_name not in ('ASSEMBLEPERSON',
                          'ASSEMBLEDATE') 
 ORDER BY b.column_id";   
            }

            else if (table_name == "SP_WORKSHOPWELD_VIEW")
            {
                sql = @"select c.column_name,
       c.comments as 项目,
       (SELECT data_type
          FROM User_Tab_Cols u
         where table_name = upper('SP_WORKSHOPWELD_VIEW')
           and u.column_name = c.column_name) as 类型
  from user_col_comments c
  join user_tab_columns b
    ON (c.table_name = b.table_name and c.column_name = b.column_name)
 where c.table_name = upper('SP_WORKSHOPWELD_VIEW')
and c.column_name not in ('WELDER',
                          'WELDDATE')
 ORDER BY b.column_id";
            }
            else if (table_name == "SP_WORKSHOTOQC_VIEW")
            {
                sql = @"select c.column_name,
       c.comments as 项目,
       (SELECT data_type
          FROM User_Tab_Cols u
         where table_name = upper('SP_WORKSHOTOQC_VIEW')
           and u.column_name = c.column_name) as 类型
  from user_col_comments c
  join user_tab_columns b
    ON (c.table_name = b.table_name and c.column_name = b.column_name)
 where c.table_name = upper('SP_WORKSHOTOQC_VIEW') 
and c.column_name not in ('TOQCDATE',
                          'QCPASSDATE')
 ORDER BY b.column_id";
            }
            else if (table_name == "SP_WORKSHOPTOTREATMENT_VIEW")
            {
                sql = @"select c.column_name,
       c.comments as 项目,
       (SELECT data_type
          FROM User_Tab_Cols u
         where table_name = upper('SP_WORKSHOPTOTREATMENT_VIEW')
           and u.column_name = c.column_name) as 类型
  from user_col_comments c
  join user_tab_columns b
    ON (c.table_name = b.table_name and c.column_name = b.column_name)
 where c.table_name = upper('SP_WORKSHOPTOTREATMENT_VIEW')
and c.column_name not in ('TOTREATMENTDATE')
 ORDER BY b.column_id";
            }
            else if (table_name == "SP_WORKSHOPRECIEVE_VIEW")
            {
                sql = @"select c.column_name,
       c.comments as 项目,
       (SELECT data_type
          FROM User_Tab_Cols u
         where table_name = upper('SP_WORKSHOPRECIEVE_VIEW')
           and u.column_name = c.column_name) as 类型
  from user_col_comments c
  join user_tab_columns b
    ON (c.table_name = b.table_name and c.column_name = b.column_name)
 where c.table_name = upper('SP_WORKSHOPRECIEVE_VIEW')
and c.column_name not in ('RECIEVEDATE')
 ORDER BY b.column_id";
            }
            else if (table_name == "SP_WORKSHOPDELIVERY_VIEW")
            {
                sql = @"select c.column_name,
       c.comments as 项目,
       (SELECT data_type
          FROM User_Tab_Cols u
         where table_name = upper('SP_WORKSHOPDELIVERY_VIEW')
           and u.column_name = c.column_name) as 类型
  from user_col_comments c
  join user_tab_columns b
    ON (c.table_name = b.table_name and c.column_name = b.column_name)
 where c.table_name = upper('SP_WORKSHOPDELIVERY_VIEW')
and c.column_name not in ('DELIVERYPERSON',
                          'DELIVERYDATE')
 ORDER BY b.column_id";
            }
            else if (table_name == "SP_WORKSHOPINSTALL_VIEW")
            {
                sql = @"select c.column_name,
       c.comments as 项目,
       (SELECT data_type
          FROM User_Tab_Cols u
         where table_name = upper('SP_WORKSHOPINSTALL_VIEW')
           and u.column_name = c.column_name) as 类型
  from user_col_comments c
  join user_tab_columns b
    ON (c.table_name = b.table_name and c.column_name = b.column_name)
 where c.table_name = upper('SP_WORKSHOPINSTALL_VIEW')
and c.column_name not in ('INSTALLDATE')
 ORDER BY b.column_id";
            }
            else if (table_name == "SP_WORKSHOPTRAYNOCLASS_VIEW")
            {
                sql = @"select c.column_name,
       c.comments as 项目,
       (SELECT data_type
          FROM User_Tab_Cols u
         where table_name = upper('SP_WORKSHOPTRAYNOCLASS_VIEW')
           and u.column_name = c.column_name) as 类型
  from user_col_comments c
  join user_tab_columns b
    ON (c.table_name = b.table_name and c.column_name = b.column_name)
 where c.table_name = upper('SP_WORKSHOPTRAYNOCLASS_VIEW')
 ORDER BY b.column_id";
            }
            #endregion
            else
            {
                return;
            }

            User.DataBaseConnect(sql, ds);
            SearchDgv.DataSource = ds.Tables[0].DefaultView;
            ds.Dispose();
            SearchDgv.Columns[0].Visible = false;
            SearchDgv.Columns[1].ReadOnly = true;
            SearchDgv.Columns[2].Visible = false;
            DataGridViewComboBoxColumn colName = new DataGridViewComboBoxColumn();
            colName.HeaderText = "符号";
            colName.Name = "sign";
            colName.ReadOnly = false;
            colName.SortMode = DataGridViewColumnSortMode.Automatic;
            
            DataTable dt = new DataTable();
            dt.Columns.Add("Display", typeof(String));
            dt.Columns.Add("Value", typeof(String));

            DataRow all = dt.NewRow(); all["Display"] = ""; all["Value"] = -1; dt.Rows.Add(all);
            DataRow dr = dt.NewRow(); dr["Display"] = "="; dr["Value"] = SqlOperator.Equal; dt.Rows.Add(dr);
            DataRow dr1 = dt.NewRow(); dr1["Display"] = "<"; dr1["Value"] = SqlOperator.LessThan; dt.Rows.Add(dr1);
            DataRow dr2 = dt.NewRow(); dr2["Display"] = "<="; dr2["Value"] = SqlOperator.LessThanOrEqual; dt.Rows.Add(dr2);
            DataRow dr3 = dt.NewRow(); dr3["Display"] = "Like"; dr3["Value"] = SqlOperator.Like; dt.Rows.Add(dr3);
            DataRow dr4 = dt.NewRow(); dr4["Display"] = ">"; dr4["Value"] = SqlOperator.MoreThan; dt.Rows.Add(dr4);
            DataRow dr5 = dt.NewRow(); dr5["Display"] = ">="; dr5["Value"] = SqlOperator.MoreThanOrEqual; dt.Rows.Add(dr5);
            DataRow dr6 = dt.NewRow(); dr6["Display"] = "<>"; dr6["Value"] = SqlOperator.NotEqual; dt.Rows.Add(dr6);
            DataRow dr7 = dt.NewRow(); dr7["Display"] = "Between"; dr7["Value"] = SqlOperator.Between; dt.Rows.Add(dr7);

            colName.DataSource = dt; colName.DisplayMember = "Display"; colName.ValueMember = "Value";
            this.SearchDgv.Columns.Add(colName);

            DataGridViewTextBoxColumn tbcName = new DataGridViewTextBoxColumn();
            tbcName.HeaderText = "值";
            tbcName.Name = "value";
            this.SearchDgv.Columns.Add(tbcName);
            tbcName.Width = 180;

            DataGridViewCheckBoxColumn checkName = new DataGridViewCheckBoxColumn();
            checkName.HeaderText = "输出";
            this.SearchDgv.Columns.Add(checkName);
            checkName.Width = 40;
            for (int k = 0; k < SearchDgv.Rows.Count; k++)
            {
                ((DataGridViewCheckBoxCell)SearchDgv.Rows[k].Cells[5]).Value = true;
            }
        }

        /// <summary>
        /// 完成查询功能按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            string sqlstr = string.Empty;
            SearchCondition searchObj = new SearchCondition();
            for (int i = 0; i < SearchDgv.RowCount; i++)
            {
                SqlOperator typestr = new SqlOperator();
                string dtype = SearchDgv.Rows[i].Cells["类型"].Value.ToString();
                string fieldName = SearchDgv.Rows[i].Cells["column_name"].Value != null ? SearchDgv.Rows[i].Cells["column_name"].Value.ToString().ToUpper() : ""; ;
                string fieldtype = (SearchDgv.Rows[i].Cells["sign"] as DataGridViewComboBoxCell).Value != null ? (SearchDgv.Rows[i].Cells["sign"] as DataGridViewComboBoxCell).Value.ToString() : "";
                object  value  ;
                string fieldvalue;
                if (fieldName == "FLOWSTATUS")
                {
                    value = SearchDgv.Rows[i].Cells["value"].Value;
                    if (value != null)
                    {
                        fieldvalue = value.ToString();
                        switch (fieldvalue)
                        {
                            case "初始":
                                fieldvalue = DBConnection.GetSpoolStatus("初始");
                                break;
                            case "审核中":
                                fieldvalue = DBConnection.GetSpoolStatus("审核中");
                                break;
                            case "审核通过":
                                fieldvalue = DBConnection.GetSpoolStatus("审核通过");
                                break;
                            case "审核退回":
                                fieldvalue = DBConnection.GetSpoolStatus("审核退回");
                                break;
                            case "下料完成":
                                fieldvalue = DBConnection.GetSpoolStatus("下料完成");
                                break;
                            case "装配完成":
                                fieldvalue = DBConnection.GetSpoolStatus("装配完成");
                                break;
                            case "焊接完成":
                                fieldvalue = DBConnection.GetSpoolStatus("焊接完成");
                                break;
                            case "待验":
                                fieldvalue = DBConnection.GetSpoolStatus("待验");
                                break;

                            case "检验通过":
                                fieldvalue = DBConnection.GetSpoolStatus("检验通过");
                                break;
                            case "检验不通过":
                                fieldvalue = DBConnection.GetSpoolStatus("检验不通过");
                                break;
                            case "处理完成":
                                fieldvalue = DBConnection.GetSpoolStatus("处理完成");
                                break;
                            case "接收完成":
                                fieldvalue = DBConnection.GetSpoolStatus("接收完成");
                                break;
                            case "发放完成":
                                fieldvalue = DBConnection.GetSpoolStatus("发放完成");
                                break;
                            case "安装完成":
                                fieldvalue = DBConnection.GetSpoolStatus("安装完成");
                                break;

                            default:
                                break;
                        }
                    }
                    else
                    {
                        fieldvalue = null;
                    }
                }
                else
                {
                    fieldvalue = SearchDgv.Rows[i].Cells["value"].Value != null ? SearchDgv.Rows[i].Cells["value"].Value.ToString().ToUpper() : null;
                }
                if (string.IsNullOrEmpty(fieldtype))
                {
                    typestr = SqlOperator.Like;
                }
                else
                {
                    typestr = (SqlOperator)Enum.Parse(typeof(SqlOperator), fieldtype);
                }

                if (!string.IsNullOrEmpty(fieldvalue))
                {
                    searchObj.AddCondition(fieldName, fieldvalue, dtype, typestr, true);
                }
            }
            
            conditionSql = searchObj.BuildConditionSql();

            StringBuilder sb = new StringBuilder();
            for (int f = 0; f < this.SearchDgv.Rows.Count; f++)
            {
                string _selectValue = SearchDgv.Rows[f].Cells[5].EditedFormattedValue.ToString();
                if (_selectValue == "True")
                {
                    string FilterStr = SearchDgv.Rows[f].Cells[0].Value.ToString();
                    if (FilterStr == "FLOWSTATUS")
                    {
                        FilterStr = "(SELECT NAME FROM SP_FLOWSTATUS_TAB s WHERE s.ID=FLOWSTATUS)";
                    }
                    else if (FilterStr == "REMARK")
                    {
                        FilterStr = "replace(REMARK, chr(10), '')";
                    }
                    else if (FilterStr == "CONNECTORTYPE")
                    {
                        FilterStr = "decode(CONNECTORTYPE,'V','阀门','S','小票','O','其他',' ')";
                    }
                    else if (FilterStr == "WELDLENGTH")
                    {
                        FilterStr = "(case when WELDLENGTH > 10000 then 0 else round(WELDLENGTH,2) end) ";
                    }

                    if (table_name == "SP_WORKSHOPBLANKING_VIEW" || table_name == "SP_WORKSHOPASSEMBLY_VIEW" || table_name == "SP_WORKSHOPWELD_VIEW" || table_name == "SP_WORKSHOTOQC_VIEW" || table_name == "SP_WORKSHOPTOTREATMENT_VIEW" || table_name == "SP_WORKSHOPRECIEVE_VIEW" || table_name == "SP_WORKSHOPDELIVERY_VIEW" || table_name == "SP_WORKSHOPTRAYNOCLASS_VIEW" || table_name == "SP_NORMALPIPEWORKINGHOUR_VIEW")
                    {
                        sb.AppendFormat("{0},", FilterStr);
                    }
                    else
                    {
                        string NameStr = SearchDgv.Rows[f].Cells[1].Value.ToString();
                        sb.AppendFormat("{0}  {1},", FilterStr, NameStr);
                    }
                }
            }
            if (sb.Length<1)
            {
                MessageBox.Show("请选择要输出的字段！");
                return;
            }
            sb.Remove(sb.Length-1,1);

            this.Close();
            if (MDIForm.pMainWin.ActiveMdiChild.Text.ToString() == "加工小票概览")
            {
                connector = " and ";
                wheresql0 = string.Format("{0}{1}flowstatus in ({2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12})", conditionSql, connector,  (int)FlowState.审核通过,  (int)FlowState.下料完成, (int)FlowState.装配完成, (int)FlowState.焊接完成, (int)FlowState.待验, (int)FlowState.检验通过, (int)FlowState.检验不通过, (int)FlowState.处理完成, (int)FlowState.接收完成, (int)FlowState.发放完成, (int)FlowState.安装完成);
            }
            else
            {
                connector = " " + "and" + " " + " flag = 'Y'" + " " + "and" + " ";
                wheresql0 = string.Format("{0}{1}flowstatus in ({2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15})", conditionSql, connector, (int)FlowState.初始, (int)FlowState.审核中, (int)FlowState.审核通过, (int)FlowState.审核退回, (int)FlowState.下料完成, (int)FlowState.装配完成, (int)FlowState.焊接完成, (int)FlowState.待验, (int)FlowState.检验通过, (int)FlowState.检验不通过, (int)FlowState.处理完成, (int)FlowState.接收完成, (int)FlowState.发放完成, (int)FlowState.安装完成);
            }
            string con = " " + "and" + " " + " flag = 'Y'";
            string constr = " and " + "(materialname like '%主管%' OR materialname like '%支管%')" + " and " + " flag = 'Y'";
            string wheresql6 = string.Format("{0}", conditionSql);

            string wheresql8 = string.Format("{0}{1}", conditionSql,con);
            string wheresql9 = string.Format("{0}{1}",conditionSql,constr);
            int count = 0;
            sqlstr = "SELECT " + sb + " FROM " + table_name;
            if (MDIForm.pMainWin.ActiveMdiChild.Text == "设计小票概览")
            {
                User.DataBaseConnect(sqlstr + wheresql0 + " order by spoolname", dset);
                ToolStatusShow();
            }

            else if (MDIForm.pMainWin.ActiveMdiChild.Text == "加工小票概览")
            {
                User.DataBaseConnect(sqlstr + wheresql0 +" order by spoolname", dset);
                ToolStatusShow();

            }
            else if (MDIForm.pMainWin.ActiveMdiChild.Text == "阀门信息概览")
            {
                User.DataBaseConnect(sqlstr + wheresql6, dset);
                ToolStatusShow();
            }

            else if (MDIForm.pMainWin.ActiveMdiChild.Text == "质检小票概览")
            {
                User.DataBaseConnect(sqlstr + wheresql0 + " order by spoolname", dset);
                ToolStatusShow();

            }

            else if (MDIForm.pMainWin.ActiveMdiChild.Text == "管路统计信息")
            {
                User.DataBaseConnect(sqlstr + wheresql6 + " order by spoolname", dset);
                try
                {
                    ((DataGridView)(MDIForm.pMainWin.ActiveMdiChild.Controls["OverViewdgv"])).DataSource = dset.Tables[0].DefaultView;
                    ((ContextMenuStrip)(MDIForm.pMainWin.ActiveMdiChild.Controls["OverViewdgv"].ContextMenuStrip)).Enabled = true;
                    User.GetPipeMaterialString(wheresql6);
                    dset.Dispose();
                    count = ((DataGridView)(MDIForm.pMainWin.ActiveMdiChild.Controls["OverViewdgv"])).Rows.Count;
                    ((ToolStrip)(MDIForm.pMainWin.ActiveMdiChild.Controls["statusStrip1"])).Items["toolStripStatusLabel1"].Text = string.Format(" 当前总记录数：{0}个", count);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                    return;
                }
            }


            else if (MDIForm.pMainWin.ActiveMdiChild.Text == "管路附件统计信息")
            {
                User.DataBaseConnect(sqlstr + wheresql6 + " order by spoolname", dset);
                try
                {
                    ((DataGridView)(MDIForm.pMainWin.ActiveMdiChild.Controls["OverViewdgv"])).DataSource = dset.Tables[0].DefaultView;
                    ((ContextMenuStrip)(MDIForm.pMainWin.ActiveMdiChild.Controls["OverViewdgv"].ContextMenuStrip)).Enabled = true;
                    User.GetPipePartString(wheresql6);
                    dset.Dispose();
                    count = ((DataGridView)(MDIForm.pMainWin.ActiveMdiChild.Controls["OverViewdgv"])).Rows.Count;
                    ((ToolStrip)(MDIForm.pMainWin.ActiveMdiChild.Controls["statusStrip1"])).Items["toolStripStatusLabel1"].Text = string.Format(" 当前总记录数：{0}个", count);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                    return;
                }
            }

            else if (MDIForm.pMainWin.ActiveMdiChild.Text == "材料信息概览")
            {
                User.DataBaseConnect(sqlstr + wheresql6 + " order by spoolname", dset);
                try
                {
                    ((DataGridView)(MDIForm.pMainWin.ActiveMdiChild.Controls["OverViewdgv"])).DataSource = dset.Tables[0].DefaultView;
                    ((ContextMenuStrip)(MDIForm.pMainWin.ActiveMdiChild.Controls["OverViewdgv"].ContextMenuStrip)).Enabled = true;
                    dset.Dispose();
                    count = ((DataGridView)(MDIForm.pMainWin.ActiveMdiChild.Controls["OverViewdgv"])).Rows.Count;
                    ((ToolStrip)(MDIForm.pMainWin.ActiveMdiChild.Controls["statusStrip1"])).Items["toolStripStatusLabel1"].Text = string.Format(" 当前总记录数：{0}个", count);
                    
                    if (UserSecurity.HavingPrivilege(User.cur_user, "SPOOLMACHINEUSERS"))
                    {
                        ((DataGridView)(MDIForm.pMainWin.ActiveMdiChild.Controls["OverViewdgv"])).EditMode = DataGridViewEditMode.EditOnEnter;
                        foreach (DataGridViewColumn dgvc in ((DataGridView)(MDIForm.pMainWin.ActiveMdiChild.Controls["OverViewdgv"])).Columns)
                        {
                            if (dgvc.Name != "炉批号" && dgvc.Name != "证书号" && dgvc.Name != "焊接工艺号")
                            {
                                dgvc.ReadOnly = true;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                    return;
                }
            }

            else if (MDIForm.pMainWin.ActiveMdiChild.Text == "连接件信息概览")
            {
                User.DataBaseConnect(sqlstr + wheresql6 + " order by spoolname", dset);
                ToolStatusShow();
            }

            else if (MDIForm.pMainWin.ActiveMdiChild.Text == "加工信息概览")
            {
                sqlstr = "SELECT " + sb + " FROM " + table_name;
                User.DataBaseConnect(sqlstr + wheresql8 + " order by spoolname", dset);
                ToolStatusShow();
            }

            else if (MDIForm.pMainWin.ActiveMdiChild.Text == "焊接信息概览")
            {
                User.DataBaseConnect(sqlstr + wheresql6 + " order by spoolname", dset);
                try
                {
                    ((DataGridView)(MDIForm.pMainWin.ActiveMdiChild.Controls["OverViewdgv"])).DataSource = dset.Tables[0].DefaultView;
                    ((ContextMenuStrip)(MDIForm.pMainWin.ActiveMdiChild.Controls["OverViewdgv"].ContextMenuStrip)).Enabled = true;
                    dset.Dispose();
                    count = ((DataGridView)(MDIForm.pMainWin.ActiveMdiChild.Controls["OverViewdgv"])).Rows.Count;
                    ((ToolStrip)(MDIForm.pMainWin.ActiveMdiChild.Controls["statusStrip1"])).Items["toolStripStatusLabel1"].Text = string.Format(" 当前总记录数：{0}个", count);
                    if (UserSecurity.HavingPrivilege(User.cur_user, "SPOOLMACHINEUSERS"))
                    {
                        ((DataGridView)(MDIForm.pMainWin.ActiveMdiChild.Controls["OverViewdgv"])).EditMode = DataGridViewEditMode.EditOnEnter;
                        foreach (DataGridViewColumn dgvc in ((DataGridView)(MDIForm.pMainWin.ActiveMdiChild.Controls["OverViewdgv"])).Columns)
                        {
                            if (dgvc.Name != "焊接人")
                            {
                                dgvc.ReadOnly = true;
                            }
                        }
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                    return;
                }
            }

            else if (MDIForm.pMainWin.ActiveMdiChild.Text == "已发图纸材料定额表")
            {
                User.DataBaseConnect(sqlstr + wheresql8, dset);
                ToolStatusShow();
            }

            else if (MDIForm.pMainWin.ActiveMdiChild.Text == "下料工时定额")
            {
                User.DataBaseConnect(sqlstr + wheresql6 + " ORDER BY SPOOLNAME", dset);
                GongShiStatusShow();
            }

            else if (MDIForm.pMainWin.ActiveMdiChild.Text == "装配工时定额")
            {
                User.DataBaseConnect(sqlstr + wheresql8 + " order by spoolname", dset);
                GongShiStatusShow();
            }

            else if (MDIForm.pMainWin.ActiveMdiChild.Text == "焊接工时定额")
            {
                User.DataBaseConnect(sqlstr + wheresql8 + " order by spoolname", dset);
                GongShiStatusShow();

            }

            else if (MDIForm.pMainWin.ActiveMdiChild.Text == "料场工时定额")
            {
                User.DataBaseConnect(sqlstr + wheresql8 + " order by spoolname", dset);
                GongShiStatusShow();
            }

            else if (MDIForm.pMainWin.ActiveMdiChild.Text == "报验工时定额")
            {
                User.DataBaseConnect(sqlstr + wheresql8 + " order by spoolname", dset);
                GongShiStatusShow();
            }

            else if (MDIForm.pMainWin.ActiveMdiChild.Text == "压力试验工时定额")
            {
                User.DataBaseConnect(sqlstr + wheresql8 + " order by spoolname", dset);
                GongShiStatusShow();
            }
            else if (MDIForm.pMainWin.ActiveMdiChild.Text == "普通碳钢管(铜管)及普通不锈钢管总工时概览")
            {
                User.DataBaseConnect(sqlstr + wheresql6 + " order by spoolname", dset);
                try
                {
                    DataGridView dgv = (DataGridView)((MDIForm.pMainWin.ActiveMdiChild.Controls["dataGridView1"]));
                    dgv.DataSource = dset.Tables[0].DefaultView;
                    dset.Dispose();
                    count = dgv.Rows.Count;
                    ((ToolStrip)(MDIForm.pMainWin.ActiveMdiChild.Controls["statusStrip1"])).Items["toolStripStatusLabel1"].Text = string.Format(" 当前总记录数：{0}个", count);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                    return;
                }
            }
            #region 车间流程转换
            else if (MDIForm.pMainWin.ActiveMdiChild.Text == "下料信息")
            {
                templatestr = "BlankingCtl";
                sqlfinal = sqlstr + wheresql6 + " ORDER BY SPOOLNAME";
                WorkShopProcess(sqlfinal, templatestr);
            }
            else if (MDIForm.pMainWin.ActiveMdiChild.Text == "装配信息")
            {
                templatestr = "AssembleCtl";
                sqlfinal = sqlstr + wheresql6 + " ORDER BY SPOOLNAME";
                WorkShopProcess(sqlfinal, templatestr);
            }

            else if (MDIForm.pMainWin.ActiveMdiChild.Text == "焊接信息")
            {
                templatestr = "WeldCtl";
                sqlfinal = sqlstr + wheresql6 + " ORDER BY SPOOLNAME";
                WorkShopProcess(sqlfinal, templatestr);
            }

            else if (MDIForm.pMainWin.ActiveMdiChild.Text == "报验信息")
            {
                templatestr = "QCCtl";
                sqlfinal = sqlstr + wheresql6 + " ORDER BY SPOOLNAME";
                WorkShopProcess(sqlfinal, templatestr);
            }

            else if (MDIForm.pMainWin.ActiveMdiChild.Text == "处理信息")
            {
                templatestr = "TreatmentCtl";
                sqlfinal = sqlstr + wheresql6 + " ORDER BY SPOOLNAME";
                WorkShopProcess(sqlfinal, templatestr);
            }

            else if (MDIForm.pMainWin.ActiveMdiChild.Text == "料场接收信息")
            {
                templatestr = "RevieveCtl";
                sqlfinal = sqlstr + wheresql6 + " ORDER BY SPOOLNAME";
                WorkShopProcess(sqlfinal, templatestr);
            }

            else if (MDIForm.pMainWin.ActiveMdiChild.Text == "发放信息")
            {
                templatestr = "DeliveryCtl";
                sqlfinal = sqlstr + wheresql6 + " ORDER BY SPOOLNAME";
                WorkShopProcess(sqlfinal, templatestr);
            }

            else if (MDIForm.pMainWin.ActiveMdiChild.Text == "安装信息")
            {
                templatestr = "InstallCtl";
                sqlfinal = sqlstr + wheresql6 + " ORDER BY SPOOLNAME";
                WorkShopProcess(sqlfinal, templatestr);
            }


            else if (MDIForm.pMainWin.ActiveMdiChild.Text == "托盘及分类信息")
            {
                templatestr = "TrayClassCtl";
                sqlfinal = sqlstr + wheresql6 + " ORDER BY SPOOLNAME";
                WorkShopProcess(sqlfinal, templatestr);
            }
            #endregion 
        }

        /// <summary>
        /// 关闭查询窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 清空值列
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < SearchDgv.RowCount; i++)
            {
                SearchDgv.Rows[i].Cells["value"].Value = null;
            }
        }

        /// <summary>
        /// 全选或反选输出字段
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void filterbtn_Click(object sender, EventArgs e)
        {
            if (this.checkBox1.Checked == true)
            {
                this.checkBox1.Checked = false;
                for (int i = 0; i < this.SearchDgv.Rows.Count; i++)
                {
                    if ((this.SearchDgv.Rows[i].Cells[5].EditedFormattedValue.ToString().Trim()).Equals("True"))
                    {
                        ((DataGridViewCheckBoxCell)SearchDgv.Rows[i].Cells[5]).Value = false;
                    }
                }
            }
            else 
            {
                this.checkBox1.Checked = true;
                for (int i = 0; i < this.SearchDgv.Rows.Count; i++)
                {
                    if ((this.SearchDgv.Rows[i].Cells[5].EditedFormattedValue.ToString().Trim()).Equals("False"))
                    {
                        ((DataGridViewCheckBoxCell)SearchDgv.Rows[i].Cells[5]).Value = true;
                    }
                }
            }

        }

        /// <summary>
        /// 关闭查询窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            return;
        }

        /// <summary>
        /// 单击查询窗体datagridview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchDgv_Click(object sender, EventArgs e)
        {
            //string sqlstatus = "SELECT NAME FROM SPFLOWSTATUS_TAB where id not in (6,8,9,15) ORDER BY ID";
            string sqlstatus = "SELECT NAME FROM SP_FLOWSTATUS_TAB  ORDER BY ID";
            if (table_name == "SP_SPOOL_TAB" || table_name == "FLOWLOG_VIEW")
            {
                ComboBox cb = new ComboBox();
                foreach( DataGridViewRow dr in this.SearchDgv.Rows )
                {
                    if(dr.Cells[4].Selected == true)
                    {
                        int id = dr.Index;
                        if(dr.Cells[0].Value.ToString() == "FLOWSTATUS")
                        {
                             Rectangle rec = this.SearchDgv.GetCellDisplayRectangle(4, id, true);
                            cb.SetBounds(rec.Location.X, rec.Location.Y, rec.Width, rec.Height);
                            this.SearchDgv.Controls.Add(cb);
                            cb.Items.Add(string.Empty);
                            DetailInfo.Application_Code.FillComboBox.GetFlowStatus(cb,sqlstatus);
                            cb.BringToFront();
                            cb.SelectedIndexChanged += new EventHandler(cb_SelectedIndexChanged);
                        }
                        else
                        {
                            foreach (Control combo in this.SearchDgv.Controls)
                            {
                                if (combo is ComboBox)
                                {
                                    combo.Dispose();
                                }
                            }
                        }
                    }
                }

            }
        }

        void cb_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (table_name == "SP_SPOOL_TAB" || table_name == "FLOWLOG_VIEW")
            {
                foreach(DataGridViewRow dr in this.SearchDgv.Rows )
                {
                    if (dr.Cells[0].Value.ToString() == "FLOWSTATUS")
                    {
                        int i = dr.Index;
                        if (((ComboBox)sender).Text == string.Empty)
                        {
                            SearchDgv.Rows[i].Cells["value"].Value = string.Empty;
                        }
                        else if (((ComboBox)sender).Text == "初始")
                        {
                            SearchDgv.Rows[i].Cells["value"].Value = FlowState.初始;
                        }
                        else if (((ComboBox)sender).Text == "审核中")
                        {
                            SearchDgv.Rows[i].Cells["value"].Value = FlowState.审核中;
                        }
                        else if (((ComboBox)sender).Text == "审核通过")
                        {
                            SearchDgv.Rows[i].Cells["value"].Value = FlowState.审核通过;
                        }
                        else if (((ComboBox)sender).Text == "审核退回")
                        {
                            SearchDgv.Rows[i].Cells["value"].Value = FlowState.审核退回;
                        }
                        else if (((ComboBox)sender).Text == "下料完成")
                        {
                            SearchDgv.Rows[i].Cells["value"].Value = FlowState.下料完成;
                        }
                        else if (((ComboBox)sender).Text == "装配完成")
                        {
                            SearchDgv.Rows[i].Cells["value"].Value = FlowState.装配完成;
                        }
                        else if (((ComboBox)sender).Text == "焊接完成")
                        {
                            SearchDgv.Rows[i].Cells["value"].Value = FlowState.焊接完成;
                        }
                        else if (((ComboBox)sender).Text == "待验")
                        {
                            SearchDgv.Rows[i].Cells["value"].Value = FlowState.待验;
                        }
                        else if (((ComboBox)sender).Text == "处理完成")
                        {
                            SearchDgv.Rows[i].Cells["value"].Value = FlowState.处理完成;
                        }
                        else if (((ComboBox)sender).Text == "检验通过")
                        {
                            SearchDgv.Rows[i].Cells["value"].Value = FlowState.检验通过;
                        }
                        else if (((ComboBox)sender).Text == "检验不通过")
                        {
                            SearchDgv.Rows[i].Cells["value"].Value = FlowState.检验不通过;
                        }
                        else if (((ComboBox)sender).Text == "接收完成")
                        {
                            SearchDgv.Rows[i].Cells["value"].Value = FlowState.接收完成;
                        }
                        else if (((ComboBox)sender).Text == "发放完成")
                        {
                            SearchDgv.Rows[i].Cells["value"].Value = FlowState.发放完成;
                        }
                        else if (((ComboBox)sender).Text == "安装完成")
                        {
                            SearchDgv.Rows[i].Cells["value"].Value = FlowState.安装完成;
                        }
                        

                        //if (((ComboBox)sender).Text == string.Empty)
                        //{
                        //    SearchDgv.Rows[i].Cells["value"].Value = string.Empty;
                        //}
                        //else if (((ComboBox)sender).Text == "初始")
                        //{
                        //    SearchDgv.Rows[i].Cells["value"].Value = FlowState.初始;
                        //}
                        //else if (((ComboBox)sender).Text == "待审")
                        //{
                        //    SearchDgv.Rows[i].Cells["value"].Value = FlowState.待审;
                        //}
                        //else if (((ComboBox)sender).Text == "审核通过")
                        //{
                        //    SearchDgv.Rows[i].Cells["value"].Value = FlowState.审核通过;
                        //}

                        //else if (((ComboBox)sender).Text == "反馈设计")
                        //{
                        //    SearchDgv.Rows[i].Cells["value"].Value = FlowState.反馈设计;
                        //}

                        //else if (((ComboBox)sender).Text == "审核反馈")
                        //{
                        //    SearchDgv.Rows[i].Cells["value"].Value = FlowState.审核反馈;
                        //}

                        //else if (((ComboBox)sender).Text == "处理反馈设计")
                        //{
                        //    SearchDgv.Rows[i].Cells["value"].Value = FlowState.处理反馈设计;
                        //}
                        //else if (((ComboBox)sender).Text == "处理审核反馈")
                        //{
                        //    SearchDgv.Rows[i].Cells["value"].Value = FlowState.处理审核反馈;
                        //}

                        //else if (((ComboBox)sender).Text == "待分配")
                        //{
                        //    SearchDgv.Rows[i].Cells["value"].Value = FlowState.待分配;
                        //}

                        //else if (((ComboBox)sender).Text == "加工中")
                        //{
                        //    SearchDgv.Rows[i].Cells["value"].Value = FlowState.加工中;
                        //}

                        //else if (((ComboBox)sender).Text == "待验")
                        //{
                        //    SearchDgv.Rows[i].Cells["value"].Value = FlowState.待验;
                        //}

                        //else if (((ComboBox)sender).Text == "检验通过待安装")
                        //{
                        //    SearchDgv.Rows[i].Cells["value"].Value = FlowState.检验通过待安装;
                        //}

                        //else if (((ComboBox)sender).Text == "安装中")
                        //{
                        //    SearchDgv.Rows[i].Cells["value"].Value = FlowState.安装中;
                        //}

                        //else if (((ComboBox)sender).Text == "待调试")
                        //{
                        //    SearchDgv.Rows[i].Cells["value"].Value = FlowState.待调试;
                        //}

                        //else if (((ComboBox)sender).Text == "调试完成")
                        //{
                        //    SearchDgv.Rows[i].Cells["value"].Value = FlowState.调试完成;
                        //}

                        //else if (((ComboBox)sender).Text == "调试失败")
                        //{
                        //    SearchDgv.Rows[i].Cells["value"].Value = FlowState.调试失败;
                        //}
                    }
                }
            }
        }

        /// <summary>
        /// 垂直或水平拉动滚动条释放combobox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchDgv_Scroll(object sender, ScrollEventArgs e)
        {
            foreach (Control cb in this.SearchDgv.Controls)
            {
                if (cb is ComboBox)
                {
                    cb.Dispose();
                }
            }
        }

        /// <summary>
        /// 改变列宽释放combobox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SearchDgv_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            foreach (Control cb in this.SearchDgv.Controls)
            {
                if (cb is ComboBox)
                {
                    cb.Dispose();
                }
            }
        }

        private void ToolStatusShow()
        {
            try
            {
                ((DataGridView)(MDIForm.pMainWin.ActiveMdiChild.Controls["OverViewdgv"])).DataSource = dset.Tables[0].DefaultView;
                ((ContextMenuStrip)(MDIForm.pMainWin.ActiveMdiChild.Controls["OverViewdgv"].ContextMenuStrip)).Enabled = true;
                dset.Dispose();
                int count = ((DataGridView)(MDIForm.pMainWin.ActiveMdiChild.Controls["OverViewdgv"])).Rows.Count;
                ((ToolStrip)(MDIForm.pMainWin.ActiveMdiChild.Controls["statusStrip1"])).Items["toolStripStatusLabel1"].Text = string.Format(" 当前总记录数：{0}个", count);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                return;
            }
        }
        private void GongShiStatusShow()
        {
            try
            {
                DataGridView dgv = (DataGridView)((MDIForm.pMainWin.ActiveMdiChild.Controls["splitContainer1"].Controls[1].Controls[0]));
                dgv.DataSource = dset.Tables[0].DefaultView;
                dset.Dispose();
                int count = dgv.Rows.Count;
                ((ToolStrip)(MDIForm.pMainWin.ActiveMdiChild.Controls["statusStrip1"])).Items["toolStripStatusLabel1"].Text = string.Format(" 当前总记录数：{0}个", count);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                return;
            }
        }
        private void WorkShopProcess(string sql,string template)
        {
            User.DataBaseConnect(sql, dset);
            try
            {
                DataGridView dgv = (DataGridView)((MDIForm.pMainWin.ActiveMdiChild.Controls[template].Controls["dataGridView1"]));
                dgv.DataSource = dset.Tables[0].DefaultView;
                dset.Dispose();
                int count = dgv.Rows.Count;
                ((ToolStrip)(MDIForm.pMainWin.ActiveMdiChild.Controls["statusStrip1"])).Items["toolStripStatusLabel1"].Text = string.Format(" 当前总记录数：{0}个", count);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
                return;
            }
        }

    }
}