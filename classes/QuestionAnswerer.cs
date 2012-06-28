using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using weetit_website.ServiceReference1;

namespace weetit_website
{
    public class QuestionAnswerer
    {
        public static List<List<Profile>> getAnswersInProfiles(String question)
        {
            QAServiceInterfaceClient QAClient = new QAServiceInterfaceClient();
            ProfileConstructorInterfaceClient profileClient= new ProfileConstructorInterfaceClient();
            List<questionAnswer> answersForDiffQuestions = QAClient.GetAnswerWithQuestionStructure(question).ToList();
            List<List<Profile>> allAnswers = new List<List<Profile>>();
            foreach (questionAnswer qAnswer in answersForDiffQuestions)
            {
                if (qAnswer.questiontype == utilquestionTypes.URIAsnwer)
                {
                    List<String> answers=qAnswer.objectUriList.ToList();
                    if (answers.Count == 1)
                    {
                        List<Profile> fullProfile = new List<Profile>();
                        fullProfile.Add(profileClient.ConstructProfile(answers[0], MergedServicechoiceProfile.full, 15));
                        allAnswers.Add(fullProfile);
                    }
                    else if (answers.Count < 4)
                    {
                        List<Profile> miniProfiles = new List<Profile>();
                        foreach (String answer in answers)
                            miniProfiles.Add(profileClient.ConstructProfile(answer, MergedServicechoiceProfile.mini, 7));
                        allAnswers.Add(miniProfiles);
                    }
                    else
                    {
                        List<Profile> microProfile = new List<Profile>();
                        foreach (String answer in answers)
                            microProfile.Add(profileClient.ConstructProfile(answer, MergedServicechoiceProfile.micro, 7));
                        allAnswers.Add(microProfile);
                    }
                }
                else if (qAnswer.questiontype == utilquestionTypes.literalAnswer)
                {
                    
                }
                //literal answer starts here 
            }
            return allAnswers;
        }
    }
}