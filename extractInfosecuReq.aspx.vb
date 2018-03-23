/**
 * @author Crété Jonathan
 * @email jonathan.crete@hotmail.fr
 * @create date 2018-03-23 04:10:44
 * @modify date 2018-03-23 04:10:44
 * @desc Extract of server file in VB.net
*/


Imports System.Web.Script.Serialization
Imports System.Xml
Imports System.IO
Imports Microsoft.Win32

'Selecion of method 
 Private Sub InfoSecu_InfosecuReq_Init(sender As Object, e As EventArgs) Handles Me.Init
        Dim a As String
        a = Request("a")
        Select Case LCase(a)
            Case "c" : Consignes()
            Case "l" : getLibelles()
            Case "nt" : newTruck()
            Case "last" : getLastRecorded()
            Case "gt" : getTruck()
            Case "json" : createXMLrequest()
            Case "xml" : CreateXML()
            Case "ins" : InsertTrp()
            Case "insvis" : InsertVis()
        End Select
    End Sub


Private Sub newTruck()
        Dim fname As String = Request("fname")
        Dim lname As String = Request("lname")
        Dim society As String = Request("society")
        Dim vehicule1 As String = Request("vehicule1")
        Dim vehicule2 As String = Request("vehicule2")
        Dim vehicule3 As String = Request("vehicule3")
        Dim languageSelected As String = Request("languageSelected")
        Dim type As String = Request("type")

        Dim onucode As String = Request("onucode")
        Dim loading As String = Request("loading")


        Dim svcexp As String = Request("svcexp")
        Dim svcrep As String = Request("svcrep")
        Dim svcret As String = Request("svcret")
        Dim svcres As String = Request("svcres")
        Dim svcmai As String = Request("svcmai")
        Dim svccom As String = Request("svccom")


        Dim opcha As String = Request("opcha")
        Dim opdce As String = Request("opdce")
        Dim oprce As String = Request("oprce")
        Dim opdec As String = Request("opdec")
        Dim opliv As String = Request("opliv")
        Dim optra As String = Request("optra")
        Dim vsgcode As String = String.Concat(fname.Substring(0, 3), lname.Substring(0, 3), society.Substring(0, 3))

        Dim signature = Request("signature")
        Dim Enc As New System.Text.ASCIIEncoding
        Dim sig2 As String = Enc.GetString(Convert.FromBase64String(signature))

        Dim u As New clsUser
        u.Nom = lname
        u.Prenom = fname
        u.Societe = society
        u.Langue = languageSelected
        u.insert()

        If Not IsNothing(vehicule1) Then
            If vehicule1 <> "" Then
                Dim v As New clsVehicule
                v.Immat = vehicule1
                v.Type = type
                If (v.Type = "Semi-remorque"  Or v.Type = "Semi-remorque +1 remorque") Then
                    v.Type = "Tracteur Routier"
                End If
                If (v.Type = "Camion fourgon avec remorque") Then
                    v.Type = "Camion fourgon"
                End If
                v.Trailer = "False"
                v.Societe = society
                v.insert()

                Dim m As New clsMouvements
                m.UserId = u.ID
                m.VehiculeId = v.ID

                m.Onu = onucode

                m.Expeditions = IIf(svcexp = "1", True, False)
                m.Reception = IIf(svcrep = "1", True, False)
                m.Retours = IIf(svcret = "1", True, False)
                m.Restaurant = IIf(svcres = "1", True, False)
                m.Maintenance = IIf(svcmai = "1", True, False)
                m.Comite = IIf(svccom = "1", True, False)

                m.Chargement = IIf(opcha = "1", True, False)
                m.Depose_caisse = IIf(opdce = "1", True, False)
                m.Reprise_caisse = IIf(oprce = "1", True, False)
                m.Dechargement = IIf(opdec = "1", True, False)
                m.Livraison = IIf(opliv = "1", True, False)
                m.Travaux = IIf(optra = "1", True, False)

                m.Charg = IIf(loading = "1", True, False)
                m.Signature = signature
                m.insert()

            End If
        End If



    End Sub



'Example of Job Class clsUser 
Public Class clsUser
    Dim _No As Long = 0
    Dim _ID As String = ""
    Dim _Nom As String = ""
    Dim _Prenom As String = ""
    Dim _Societe As String = ""
    Dim _DateConsigne As Date = Date.Now
    Dim _Langue As String = ""


    'Properties to be completed or acquired
    Public Property No() As String
        Get
            Return CStr(_No)
        End Get
        Set(ByVal value As String)
            _No = CLng(value)
        End Set
    End Property


    Public Property Nom() As String
        Get
            Return _Nom
        End Get
        Set(ByVal value As String)
            _Nom = value
            _ID = IDBuild()
        End Set
    End Property

    Public Property Prenom() As String
        Get
            Return _Prenom
        End Get
        Set(ByVal value As String)
            _Prenom = value
            _ID = IDBuild()
        End Set
    End Property

    Public ReadOnly Property ID() As String
        Get
            Return _ID
        End Get

    End Property


    Public Property Societe() As String
        Get
            Return _Societe
        End Get
        Set(ByVal value As String)
            _Societe = value
            _ID = IDBuild()
        End Set
    End Property

    Public Property DateConsigne() As String
        Get
            Return _DateConsigne
        End Get
        Set(ByVal value As String)
            _DateConsigne = value
        End Set
    End Property
    Public Property Langue() As String
        Get
            Return _Langue
        End Get
        Set(ByVal value As String)
            _Langue = value
        End Set
    End Property


'Example of function Insert into T_users table
Function insert() As Long
        Dim good As Boolean = True
        If Trim(_Nom) = "" Then good = False
        If Trim(_Prenom) = "" Then good = False
        If Not good Then Return 0

        Dim i As Long = 0

        ' Insert into T_USERS
        Dim oCon As System.Data.Odbc.OdbcConnection = InfoSecuOdbcNet.Open()
        If IsNothing(oCon) Then
            Return 0
        End If
        Dim oCmd As New Data.Odbc.OdbcCommand
        With oCmd
            .Connection = oCon
            .CommandText = "Insert into T_Users (nom,prenom,societe,id,DateConsigne,Langue) values (?,?,?,?,?,?)"
            .CommandType = Data.CommandType.Text
            .Parameters.Add("@nom", Data.Odbc.OdbcType.Text).Value = _Nom
            .Parameters.Add("@prenom", Data.Odbc.OdbcType.Text).Value = _Prenom
            .Parameters.Add("@societe", Data.Odbc.OdbcType.Text).Value = _Societe
            .Parameters.Add("@id", Data.Odbc.OdbcType.Text).Value = _ID
            .Parameters.Add("@dateconsigne", Data.Odbc.OdbcType.DateTime).Value = _DateConsigne
            .Parameters.Add("@langue", Data.Odbc.OdbcType.Text).Value = _Langue


        End With

        Do
            oCmd.Parameters("@id").Value = _ID
            Try
                oCmd.ExecuteNonQuery()
                Exit Do
            Catch ex As System.Data.Odbc.OdbcException
                DebugLog.Write("InfoSecuReq.aspx", "clsUser/Insert/1" & vbCrLf & ex.Message & vbCrLf & oCmd.CommandText & vbCrLf & "<" & _Nom & "|" & _Prenom & "|" & _Societe & "|" & _ID & "|" & _DateConsigne & "|" & _Langue & "|" & ">")
                i = i + 1
                _ID = IDBuild() & i
                If i > 10 Then
                    oCon.Close()
                    Return 0
                End If
            Catch ex As Exception
                DebugLog.Write("InfoSecuReq.aspx", "clsUser/Insert/2" & vbCrLf & ex.Message)
                oCon.Close()
                Return 0
            End Try
        Loop
        oCon.Close()
        Return 1
    End Function
