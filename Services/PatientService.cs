using System;
using Core.Models;
using Core.Extensions;

namespace Core.Services
{
    public class PatientService
    {
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
    }
}
