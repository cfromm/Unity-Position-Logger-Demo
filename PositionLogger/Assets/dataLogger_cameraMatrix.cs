using UnityEngine;
using System;
using System.IO;
using System.Text;


/// <summary>
/// This script records data each frame in a text file in the following tab-delimited format
/// Frame   Start	CameraPosX	CameraPosY	CameraPosZ	CameraMatR0C0...CameraMatR3C3	TargetPosX  TargetPosY   TargetPosZ				
///------------------------------------------------------------------------------
/// 1       1241.806	float		float		float   	float		float		float	    float	 float	
/// 2       4619.335	float		float		float   	float		float		float	    float	 float
/// </remark>
/// 

namespace PupilLabs
{
    public class dataLogger : MonoBehaviour
    {
        
        public string FolderName = "D:\\Data\\WriteoutTest";
        private string OutputDir;

        //Things you want to write out, set them in the inspector
        public Transform cameraTransform;

        //Gives user control over when to start and stop recording, trigger this with spacebar;
        public bool startWriting;

        //Initialize some containers
        FileStream streams;
        FileStream trialStreams;
        StringBuilder stringBuilder = new StringBuilder();
        String writeString;
        Byte[] writebytes;


        void Start()
        {
            // create a folder 
            string OutputDir = Path.Combine(FolderName, string.Concat(DateTime.Now.ToString("MM-dd-yyyy-HH-mm")));
            Directory.CreateDirectory(OutputDir);

            // create a file to record data
            String trialOutput = Path.Combine(OutputDir, DateTime.Now.ToString("yyyy-MM-dd-HH-mm") + ".txt");
            //String trialOutput = Path.Combine(OutputDir, "test.txt");
            trialStreams = new FileStream(trialOutput, FileMode.Create, FileAccess.Write);


            //Call the function below to write the column names
            WriteHeader();

        }


        void WriteHeader()
        {

            //output file-- order of column names here should match the order you use when writing out each value 
            stringBuilder.Length = 0;

            //add column names
            stringBuilder.Append(
                "FrameNumber\t" 
                + "StartTime\t" 
                + "cameraPos_X\t" 
                + "cameraPos_Y\t"
                + "cameraPos_Z\t"
                + "cameraMat_R0C0\t"
                + "cameraMat_R0C1\t"
                + "cameraMat_R0C2\t"
                + "cameraMat_R0C3\t"
                + "cameraMat_R1C0\t"
                + "cameraMat_R1C1\t"
                + "cameraMat_R1C2\t"
                + "cameraMat_R1C3\t"
                + "cameraMat_R2C0\t"
                + "cameraMat_R2C1\t"
                + "cameraMat_R2C2\t"
                + "cameraMat_R2C3\t"
                + "cameraMat_R3C0\t"
                + "cameraMat_R3C1\t"
                + "cameraMat_R3C2\t"
                + "cameraMat_R3C3\t"
                + "targetPos_X\t" 
                + "targetPos_Y\t"
                + "targetPos_Z\t"
                + Environment.NewLine
                            );


            writeString = stringBuilder.ToString();
            writebytes = Encoding.ASCII.GetBytes(writeString);
            trialStreams.Write(writebytes, 0, writebytes.Length);

        }


        void WriteFile()
        {

            
            Vector4 camRow0 = cameraTransform.localToWorldMatrix.GetRow(0);
            Vector4 camRow1 = cameraTransform.localToWorldMatrix.GetRow(1);
            Vector4 camRow2 = cameraTransform.localToWorldMatrix.GetRow(2);
            Vector4 camRow3 = cameraTransform.localToWorldMatrix.GetRow(3);

            stringBuilder.Length = 0;
            stringBuilder.Append(
                        Time.frameCount + "\t"
                        + Time.time * 1000 + "\t"
                        + cameraTransform.position.x.ToString() + "\t"
                        + cameraTransform.position.y.ToString() + "\t"
                        + cameraTransform.position.z.ToString() + "\t"

                        + camRow0[0].ToString() + "\t"
                        + camRow0[1].ToString() + "\t"
                        + camRow0[2].ToString() + "\t"
                        + camRow0[3].ToString() + "\t"

                        + camRow1[0].ToString() + "\t"
                        + camRow1[1].ToString() + "\t"
                        + camRow1[2].ToString() + "\t"
                        + camRow1[3].ToString() + "\t"

                        + camRow2[0].ToString() + "\t"
                        + camRow2[1].ToString() + "\t"
                        + camRow2[2].ToString() + "\t"
                        + camRow2[3].ToString() + "\t"

                        + camRow3[0].ToString() + "\t"
                        + camRow3[1].ToString() + "\t"
                        + camRow3[2].ToString() + "\t"
                        + camRow3[3].ToString() + "\t"

                        + Environment.NewLine
                    );
            writeString = stringBuilder.ToString();
            writebytes = Encoding.ASCII.GetBytes(writeString);
            trialStreams.Write(writebytes, 0, writebytes.Length);
        }

        public void Update()
        {   
            
            //Use spacebar to initiate/stop recording values, you can change this if you want 
            if (Input.GetKeyDown(KeyCode.Space))
            {
                startWriting = !startWriting;
                if (startWriting)
                {
                    Debug.Log("Start writing");
                }
                else
                {
                    Debug.Log("Stop writing");
                }
            }
            if (startWriting) ;
            {
                WriteFile();
            }


        }

        public void OnApplicationQuit()
        {
            trialStreams.Close();

        }

    }
}