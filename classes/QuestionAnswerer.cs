using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using weetit_website.ServiceReference1;

namespace weetit_website.classes
{
    public class QuestionAnswerer
    {
        public static List<KeyValuePair<questionAnswer,List<Profile>>> getAnswersInProfiles(String question)
        {
            QAServiceInterfaceClient QAClient = new QAServiceInterfaceClient();
            ProfileConstructorInterfaceClient profileClient = new ProfileConstructorInterfaceClient();
            List<questionAnswer> answersForDiffQuestions = QAClient.GetAnswerWithQuestionStructure(question).ToList();
            List<List<Profile>> allAnswers = new List<List<Profile>>();
            List<KeyValuePair<questionAnswer, List<Profile>>> output = new List<KeyValuePair<questionAnswer, List<Profile>>>();
            bool isKeywordSearch = true;
            foreach (questionAnswer qAnswer in answersForDiffQuestions)
                if (qAnswer.predicateUriList.Count != 0 || qAnswer.subjectUriList.Count != 0)
                {
                    isKeywordSearch = false;
                    break;
                }
            if (isKeywordSearch == false)
            {
                foreach (questionAnswer qAnswer in answersForDiffQuestions)
                {
                    if (qAnswer.questiontype == utilquestionTypes.URIAsnwer)
                    {
                        List<String> answers = qAnswer.objectUriList.ToList();
                        if (answers.Count == 1)
                        {
                            List<Profile> fullProfile = new List<Profile>();
                            fullProfile.Add(profileClient.ConstructProfile(answers[0], MergedServicechoiceProfile.full, 15));
                            KeyValuePair<questionAnswer, List<Profile>> questionNAnswer = new KeyValuePair<questionAnswer, List<Profile>>(qAnswer, fullProfile);
                            output.Add(questionNAnswer);
                        }
                        else if (answers.Count < 4 && answers.Count>1)
                        {
                            List<Profile> miniProfiles = new List<Profile>();
                            foreach (String answer in answers)
                                miniProfiles.Add(profileClient.ConstructProfile(answer, MergedServicechoiceProfile.mini, 7));
                            KeyValuePair<questionAnswer, List<Profile>> questionNAnswer = new KeyValuePair<questionAnswer, List<Profile>>(qAnswer, miniProfiles);
                            output.Add(questionNAnswer);
                        }
                        else
                        {
                            List<Profile> microProfile = new List<Profile>();
                            foreach (String answer in answers)
                                microProfile.Add(profileClient.ConstructProfile(answer, MergedServicechoiceProfile.micro, 7));
                            KeyValuePair<questionAnswer, List<Profile>> questionNAnswer = new KeyValuePair<questionAnswer, List<Profile>>(qAnswer, microProfile);
                            output.Add(questionNAnswer);
                        }
                    }
                    else if (qAnswer.questiontype == utilquestionTypes.literalAnswer)
                    {
                        List<Profile> LPLIST = new List<Profile>();
                        // string h= qAnswer.subjectUriList.Keys.ToList()[0];
                        LPLIST.Add(profileClient.ConstructLiteralProfile(qAnswer.subjectUriList.Keys.ToList()[0], qAnswer.predicateLabelList.Keys.ToList()[0], qAnswer.subjectLabelList.Keys.ToList()[0], "jkfjk", qAnswer.objectUriList[0], qAnswer.predicateUriList.Keys.ToList()[0]));

                        KeyValuePair<questionAnswer, List<Profile>> questionNAnswer = new KeyValuePair<questionAnswer, List<Profile>>(qAnswer, LPLIST);
                        output.Add(questionNAnswer);


                    }

                }
            }
            else
            {
                List<String> answers = new List<String>();
                foreach (questionAnswer answer in answersForDiffQuestions)
                    if (answer.objectUriList.Length > 0)
                        answers.Add(answer.objectUriList[0]);
                if (answers.Count == 1)
                {
                    List<Profile> fullProfile = new List<Profile>();
                    fullProfile.Add(profileClient.ConstructProfile(answers[0], MergedServicechoiceProfile.full, 15));
                    KeyValuePair<questionAnswer, List<Profile>> questionNAnswer = new KeyValuePair<questionAnswer, List<Profile>>(answersForDiffQuestions[0], fullProfile);
                    output.Add(questionNAnswer);
                }
                else if (answers.Count < 4 && answers.Count>1)
                {
                    List<Profile> miniProfiles = new List<Profile>();
                    foreach (String answer in answers)
                    {
                        miniProfiles.Add(profileClient.ConstructProfile(answer, MergedServicechoiceProfile.mini, 7));
                        KeyValuePair<questionAnswer, List<Profile>> questionNAnswer = new KeyValuePair<questionAnswer, List<Profile>>(answersForDiffQuestions.ElementAt(answers.IndexOf(answer)), miniProfiles);
                        output.Add(questionNAnswer);
                    }
                    
                }
                else
                {
                    List<Profile> microProfile = new List<Profile>();
                    foreach (String answer in answers)
                    {
                        microProfile.Add(profileClient.ConstructProfile(answer, MergedServicechoiceProfile.micro, 7));
                        KeyValuePair<questionAnswer, List<Profile>> questionNAnswer = new KeyValuePair<questionAnswer, List<Profile>>(answersForDiffQuestions.ElementAt(answers.IndexOf(answer)), microProfile);
                        output.Add(questionNAnswer);
                    }
                }
            }
            return output;
        }
    }
}