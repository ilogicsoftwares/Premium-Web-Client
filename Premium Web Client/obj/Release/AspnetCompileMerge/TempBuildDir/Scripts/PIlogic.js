//Premium Web Client Version 1.0 Por Richard Aguirre - Ilogic Softwares C.A. 
//este es el objeto que se resive desde el servidor, siempre es una tabla convertida a json[]
//Ejemplo de uso
//1.-Iniciar El serivicio con los datos el usuario de la base de datos
var Jsonres = {};
var JsonresFirst = new Object();
var DataBase;
var Vserver;
var Vuser;
var JsonTable = new Object();
var Getmessage = new MessagePub();
//el ultimo mensaje que se envio
var lastmessage=new MessagePub();
//variable global del pubnub
var pubnub
//Funcion para iniciar el servicio
function StartService(pubkey,subkey,dataBase,vserver,vuser){
    DataBase = dataBase;
    Vserver = vserver;
    Vuser = vuser;

pubnub = PUBNUB.init({
publish_key: pubkey,
subscribe_key: subkey,
error: function (error) {
console.log('Error:', error);
}
})
Subscribe(Vuser);
}

// Este es la clase que entiende el servidor como comando...
function MessagePub () {
   
    this.Type="",
    this.Msg="",
    this.Table="",
    this.Camps="",
    this.Where="",
    this.Values="",
    this.From=Vuser,
    this.Function=""
    this.DataBase=DataBase
     // la base de datos puede ser "Administrativo,Clinica,Contabilidad,Restaurant,Nomina"
}
//Clase para obtener la columnas de json
function Columna(){
    this.name="",
    this.type="",
    this.width=100
    this.title=""
}
//Funcion para retornar un json[] de las columnas de un Jsonres
function CargarColumnas(JsonObject){
    var Cols=[];
   var eltipo="text";
    var eltamaño=50;
    var firstelement=JsonObject[0];
for (var i in firstelement) {
     var newcol=new Columna();
    newcol.name=i;
    newcol.title= i.charAt(0).toUpperCase() + i.slice(1);
   var tipo=Object.prototype.toString.call(firstelement[i]);
   if (tipo.includes("String"))
    {
        eltipo="text"
        eltamaño=300
    }
    if (tipo.includes("Number"))
    {
        eltipo="number"
        eltamaño=100
    }
    if (tipo.includes("Date"))
    {
        eltipo="date"
        eltamaño=80
    }
    
    newcol.type=eltipo;
    newcol.width=eltamaño;
    Cols.push(newcol)
}
    Cols.push({ type: "control", editButton: false})
  return JSON.parse (JSON.stringify(Cols));  
}
//funcion para enviar una clase messagepub como comando al servidor
function SendCommand(messagePub){
    var jsonstr=JSON.stringify(messagePub);
    pubnub.publish({
    channel : Vserver,
    message : jsonstr,
    callback : function(m){
        console.log(m)
        lastmessage=JSON.parse(jsonstr);
    }
});
}
function UseDb(dbName) {
    messageconnect = new MessagePub();
    messageconnect.Type("UseDb");
    messageconnect.Msg(dbName);
    SendCommand(messageconnect);
}

//Fucion para escuchar en un canal
function Subscribe(chanel){
    pubnub.subscribe({
    channel : chanel,
    message : function(m){
        console.log(m);
        if (m.includes("Error")){
            alert(m);
        }
      
        Getmessage = JSON.parse(m);
        Jsonres[Getmessage.Table] = JSON.parse(Getmessage.Msg);
  
        CreateTable(Getmessage.Table);
        JsonresFirst=Jsonres[0];
         try{
         
             ExecuteFunction(Getmessage.Function);
          
         
      }catch(err){
          alert("Ocurrio un error al ejecutar la funcion definidad como " + Getmessage.Function + "().");
      } 
        
    },
    error : function (error) {
        // Handle error here
        console.log(JSON.stringify(error));
    }
});
}
//Funcion que crea una tabla tipo json con los mensajes del servidor
function CreateTable(tablename){
    var tablex = tablename + "Data";
    eval(tablex + "={};")
    eval(tablex + "=Jsonres;");
   
}

    
//Funcion que regresa un campo con una condicion especifica
function SimpleFilter(camps,table,where,callbackfunction){
var nmessage=new MessagePub();
           
            nmessage.Type="Select";
            nmessage.Camps=camps;
            nmessage.Table=table;
            nmessage.Where=where;
            nmessage.Function=callbackfunction;
      
            SendCommand(nmessage);
}

//Funcion para Enviar un Comando sql Al servidor (el comando sql, la funcion que va a ejecutar con la respuesta)
 function SendSql(sqlcommand,table,callbackfunction){
            var nmessage=new MessagePub();
            nmessage.Type="Sql";
            nmessage.Table = table;
            nmessage.Msg=sqlcommand;
            nmessage.Function=callbackfunction;
            SendCommand(nmessage);
            
         
        }
//Dibuja una tabla con los datos de un json[]
 function RenderTable(jsonres){
     $("#row3").show();
     $("#jsGrid").jsGrid({
     width: "100%",
        height: "200px",
         sorting: true,
        paging: true,
        autoload: true,
         pageSize: 15,
        pageButtonCount: 5,
        
      
 
        deleteConfirm: "Do you really want to delete the client?",
        data: jsonres,
 
       fields: CargarColumnas(jsonres)

    });
    }   
function ExecuteFunction(funcxy){
    eval(funcxy);
}