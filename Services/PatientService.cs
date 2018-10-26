using System;
using Core.Models;
using Core.Extensions;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Core.Services
{
    public class PatientService
    {
        /*Readers*/
        public Queue GetQueue(int idnt){
            Queue queue = null;

            SqlServerConnection conn = new SqlServerConnection();
            SqlDataReader dr = conn.SqlServerConnect("SELECT qs_idnt, qs_code, qs_name, qs_route, qs_concept, cn_name FROM Queues LEFT OUTER JOIN Concept ON qs_concept=cn_idnt WHERE qs_idnt=" + idnt);
            if (dr.Read())
            {
                queue = new Queue();
                queue.Id = Convert.ToInt16(dr[0]);
                queue.Code = dr[1].ToString();
                queue.Name = dr[2].ToString();
                queue.Route = dr[3].ToString();
                queue.Concept.Id = Convert.ToInt16(dr[4]);
                queue.Concept.Name = dr[5].ToString();
            }

            return queue;
        }

        public Queue GetQueue(string code)
        {
            Queue queue = null;

            SqlServerConnection conn = new SqlServerConnection();
            SqlDataReader dr = conn.SqlServerConnect("SELECT qs_idnt, qs_code, qs_name, qs_route, qs_concept, cn_name FROM Queues LEFT OUTER JOIN Concept ON qs_concept=cn_idnt WHERE qs_code='" + code + "'");
            if (dr.Read())
            {
                queue = new Queue();
                queue.Id = Convert.ToInt16(dr[0]);
                queue.Code = dr[1].ToString();
                queue.Name = dr[2].ToString();
                queue.Route = dr[3].ToString();
                queue.Concept.Id = Convert.ToInt16(dr[4]);
                queue.Concept.Name = dr[5].ToString();
            }

            return queue;
        }

        public PatientQueue GetPatientQueue(int idnt) {
            PatientQueue pq = null;

            SqlServerConnection conn = new SqlServerConnection();
            SqlDataReader dr = conn.SqlServerConnect("SELECT pq_idnt, pq_queue, pq_date, pq_time, pq_priority, pq_seen, ISNULL(NULLIF(pq_notes,''),'N/A')pq_notes, pt_idnt, ps_idnt, ps_names, ps_dob, ps_gender, bl_idnt, bl_cost, bl_amount, pq_provider, usr_name FROM PatientQueue INNER JOIN Patient ON pq_patient=pt_idnt INNER JOIN Person ON pt_person=ps_idnt LEFT OUTER JOIN Users ON pq_provider=usr_idnt LEFT OUTER JOIN Bills ON pq_bill = bl_idnt WHERE pq_idnt=" + idnt);
            if (dr.Read())
            {
                pq = new PatientQueue();
                pq.Id = Convert.ToInt16(dr[0]);
                pq.Queue.Id = Convert.ToInt16(dr[1]);
                pq.Date = Convert.ToDateTime(dr[2]);
                pq.Time = TimeSpan.Parse(dr[3].ToString());
                pq.Priority = Convert.ToInt16(dr[4]);
                pq.Seen = Convert.ToInt16(dr[5]);
                pq.Notes = dr[6].ToString();
                pq.Patient.Id = Convert.ToInt16(dr[7]);
                pq.Patient.Person.Id = Convert.ToInt16(dr[8]);
                pq.Patient.Person.Name = dr[9].ToString();
                pq.Patient.Person.DoB = Convert.ToDateTime(dr[10]);
                pq.Patient.Person.Gender = dr[11].ToString();
                pq.Bill.Id = Convert.ToInt16(dr[12]);
                pq.Bill.Cost = Convert.ToDouble(dr[13]);
                pq.Bill.Amount = Convert.ToDouble(dr[14]);
                pq.Provider.Id = Convert.ToInt16(dr[15]);
                pq.Provider.Name = dr[16].ToString();
            }

            return pq;
        }

        public List<PatientQueue> GetPatientQueues(Queue q){
            List<PatientQueue> queues = new List<PatientQueue>();

            SqlServerConnection conn = new SqlServerConnection();
            SqlDataReader dr = conn.SqlServerConnect("SELECT pq_idnt, pq_queue, pq_date, pq_time, pq_priority, pq_seen, ISNULL(NULLIF(pq_notes,''),'N/A')pq_notes, pt_idnt, ps_idnt, ps_names, ps_dob, ps_gender, bl_idnt, bl_cost, bl_amount, pq_provider, usr_name FROM PatientQueue INNER JOIN Patient ON pq_patient=pt_idnt INNER JOIN Person ON pt_person=ps_idnt LEFT OUTER JOIN Users ON pq_provider=usr_idnt LEFT OUTER JOIN Bills ON pq_bill = bl_idnt WHERE pq_seen IN (0) AND (bl_flag IS NULL OR bl_flag=1) AND pq_queue=" + q.Id + " ORDER BY pq_date, pq_priority DESC, pq_time");
            if (dr.HasRows) {
                while (dr.Read())
                {
                    PatientQueue pq = new PatientQueue
                    {
                        Id = Convert.ToInt16(dr[0]),
                        Queue = q,
                        Date = Convert.ToDateTime(dr[2]),
                        Time = TimeSpan.Parse(dr[3].ToString()),
                        Priority = Convert.ToInt16(dr[4]),
                        Seen = Convert.ToInt16(dr[5]),
                        Notes = dr[6].ToString()
                    };

                    pq.Patient.Id = Convert.ToInt16(dr[7]);
                    pq.Patient.Person.Id = Convert.ToInt16(dr[8]);
                    pq.Patient.Person.Name = dr[9].ToString();
                    pq.Patient.Person.DoB = Convert.ToDateTime(dr[10]);
                    pq.Patient.Person.Gender = dr[11].ToString();

                    pq.Bill.Id = Convert.ToInt16(dr[12]);
                    pq.Bill.Cost = Convert.ToDouble(dr[13]);
                    pq.Bill.Amount = Convert.ToDouble(dr[14]);
                    pq.Provider.Id = Convert.ToInt16(dr[15]);
                    pq.Provider.Name = dr[16].ToString();

                    queues.Add(pq);
                }
            }

            return queues;
        }

        /*Writers*/
        public Patient SavePatient(Patient patient)
        {
            SqlServerConnection conn = new SqlServerConnection();
            patient.Id = conn.SqlServerUpdate("IF NOT EXISTS (SELECT pt_idnt FROM Patient WHERE pt_idnt=" + patient.Id + ") BEGIN INSERT INTO Patient(pt_person) output INSERTED.pt_idnt VALUES (" + patient.Person.Id + ") END ELSE BEGIN UPDATE Patient SET pt_person=" + patient.Person.Id + " OUTPUT INSERTED.pt_idnt WHERE pt_idnt=" + patient.Id + " END");

            return patient;
        }

        public Person SavePerson(Person person){
            SqlServerConnection conn = new SqlServerConnection();
            person.Id = conn.SqlServerUpdate("IF NOT EXISTS (SELECT ps_idnt FROM Person WHERE ps_idnt=" + person.Id + ") BEGIN INSERT INTO Person(ps_names, ps_nickname, ps_dob, ps_gender, ps_citizen, ps_physical, ps_mobile, ps_email, ps_residence) OUTPUT INSERTED.ps_idnt VALUES ('" + person.Name + "', '" + person.NickName + "', '" + person.DoB + "', '" + person.Gender + "', '" + person.Citizen + "', '" + person.Physical + "', '" + person.Mobile + "', '" + person.Email + "', '" + person.Residence + "') END ELSE BEGIN UPDATE Person SET ps_names='" + person.Name + "', ps_nickname='" + person.NickName + "', ps_dob='" + person.DoB + "', ps_gender='" + person.Gender + "', ps_citizen='" + person.Citizen + "', ps_physical='" + person.Physical + "', ps_mobile='" + person.Mobile + "', ps_email='" + person.Email + "', ps_residence='" + person.Residence + "' OUTPUT INSERTED.ps_idnt WHERE ps_idnt=" + person.Id + " END");

            return person;
        }

        public NextOfKin SaveNextOfKin(NextOfKin kin){
            SqlServerConnection conn = new SqlServerConnection();
            kin.Id = conn.SqlServerUpdate("IF NOT EXISTS (SELECT nk_idnt FROM PersonNok WHERE nk_idnt=" + kin.Id + ") BEGIN INSERT INTO PersonNok(nk_person, nk_names, nk_relation, nk_physical, nk_mobile, nk_email) OUTPUT INSERTED.nk_idnt VALUES (" + kin.Person.Id + ", '" + kin.Name + "', '" + kin.Relation + "', '" + kin.Physical + "', '" + kin.Mobile + "', '" + kin.Email + "') END ELSE BEGIN UPDATE PersonNok SET nk_names='" + kin.Name + "', nk_relation='" + kin.Relation + "', nk_physical='" + kin.Physical + "', nk_mobile='" + kin.Mobile + "', nk_email='" + kin.Email + "' OUTPUT INSERTED.nk_idnt WHERE nk_idnt=" + kin.Id + " END");

            return kin;
        }

        public PatientQueue SavePatientQueue(PatientQueue queue){
            SqlServerConnection conn = new SqlServerConnection();
            queue.Id = conn.SqlServerUpdate("IF NOT EXISTS (SELECT pq_idnt FROM PatientQueue WHERE pq_idnt=" + queue.Id + ") BEGIN INSERT INTO PatientQueue(pq_queue, pq_patient, pq_bill, pq_priority, pq_provider, pq_notes) OUTPUT INSERTED.pq_idnt VALUES (" + queue.Queue.Id + ", " + queue.Patient.Id + ", " + queue.Bill.Id + ", " + queue.Priority + ", " + queue.Provider.Id + ", '" + queue.Notes + "') END ELSE BEGIN UPDATE PatientQueue SET pq_date='" + queue.Date + "', pq_priority=" + queue.Priority + ", pq_provider=" + queue.Provider.Id + ", pq_notes='" + queue.Notes + "' OUTPUT INSERTED.pq_idnt WHERE pq_idnt=" + queue.Id + " END");

            return queue;
        }

        public PatientQueue UpdatePatientQueueProvider(PatientQueue queue)
        {
            SqlServerConnection conn = new SqlServerConnection();
            queue.Id = conn.SqlServerUpdate("UPDATE PatientQueue SET pq_provider=" + queue.Provider.Id + " OUTPUT INSERTED.pq_idnt WHERE pq_idnt=" + queue.Id);

            return queue;
        }
    }
}
