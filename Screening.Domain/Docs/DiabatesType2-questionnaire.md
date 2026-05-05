# 60-Second Type 2 Diabetes Risk Test

**Purpose**: This quick screening tool helps assess your risk for developing type 2 diabetes or prediabetes. It is not a diagnosis but can indicate if you should speak with a healthcare provider for further testing (e.g., A1C, fasting blood glucose, or oral glucose tolerance test).

**Instructions**:
- Answer all questions honestly.
- This test is for adults aged 18 and older.
- If you are pregnant or have been diagnosed with diabetes, consult your doctor directly.
- Date: ____________________

### Your Information
Q: Question
AT: Answer Type (TextInputQuestion|SingleChoiceQuestion|MultipleChoiceQuestion)
Cs: Choices
InF: Input Format (Number|string|DateTime)
C2S: Condition to Show
CR: Calculation Role

1. Q: What is your age?
   AT: TextInputQuestion
   InF: Number
   CR: Points: <40 = 0 | 40–49 = 1 | 50–59 = 2 | 60+ = 3

2. Q: What is your gender?
   AT: SingleChoiceQuestion
   Cs:
     - Female
     - Male
   CR: Men receive 1 point

3. Q: Have you ever had gestational diabetes OR given birth to a baby weighing 9 pounds or more?
   C2S: Women only; leave blank if male
   AT: SingleChoiceQuestion
   Cs:
      - Yes
      - No
   CR:  Yes (1 point)

4. Q: Do you have a parent, brother, or sister with diabetes?
   AT: SingleChoiceQuestion
   Cs:
     - Yes
     - No
   CR:  Yes (1 point)

5. Q: Have you ever been diagnosed with high blood pressure or are you on medication for high blood pressure?
   AT: SingleChoiceQuestion
   Cs:
     - Yes
     - No
   CR:  Yes (1 point)

6. Q: Are you physically active?
   (Examples: at least 150 minutes per week of moderate activity like brisk walking)
   AT: SingleChoiceQuestion
   Cs:
     - Yes
     - No
   CR:  Yes (1 point)

7. Q: What is your height and weight? (I am not sure about which QT should I choose)
   Height: ________ cm
   Weight: ________ kg
   CR: (Points based on body weight status:
    - If you are overweight (BMI 25–29.9): 1 point
    - If you are obese (BMI 30–34.9): 2 points
    - If you are severely obese (BMI 35+): 3 points
    - BMI <25: 0 points)

### Scoring (For healthcare provider or self-calculation)
Total points: ________

**Your Risk Level**:
- **Low risk** (Score less than 5): You currently have a lower risk for type 2 diabetes. Continue healthy habits to maintain low risk.
- **High risk** (Score 5 or higher): You may have prediabetes or be at high risk for type 2 diabetes. Talk to your doctor about getting tested and ways to lower your risk through lifestyle changes.

**Recommendations** (if high risk):
Lifestyle changes like healthy eating, regular physical activity, and weight management can prevent or delay type 2 diabetes in many people.

**For healthcare provider use only**
Calculated score: ________
Risk level: [ ] Low   [ ] High
Additional notes / follow-up recommended:
