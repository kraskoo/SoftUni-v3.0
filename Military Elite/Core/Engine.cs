namespace P08MilitaryElite.Core
{
    using System;
    using System.Linq;
    using Factories;
    using Handlers;
    using Interfaces;

    public class Engine : IRunnable
    {
        private readonly IInputHandler inputHandler;
        private readonly IMilitaryRepository militaryRepository;

        public Engine(
            IInputHandler inputHandler,
            IMilitaryRepository militaryRepository)
        {
            this.inputHandler = inputHandler;
            this.militaryRepository = militaryRepository;
        }

        public Engine() : this(new InputHandler(), new MilitaryRepository())
        {
        }

        public void Run()
        {
            string input = this.inputHandler.ReadLine();
            while (!input.Equals("End"))
            {
                string[] data =
                    input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                string type = data[0];
                string[] constrainData =
                    data.Skip(1).Take(data.Length - 1).ToArray();
                try
                {
                    switch (type)
                    {
                        case "Private":
                            this.CreatePrivate(type, constrainData);
                            break;
                        case "Engineer":
                            this.CreateEngineer(type, constrainData);
                            break;
                        case "Commando":
                            this.CreateCommando(type, constrainData);
                            break;
                        case "LeutenantGeneral":
                            this.CreateLeutenantGeneral(type, constrainData);
                            break;
                        case "Spy":
                            this.CreateSpy(type, constrainData);
                            break;
                        default:
                            throw new ArgumentException("Unknown type.");
                    }
                }
                catch (ArgumentException ae)
                {
                    this.inputHandler.WriteLine(ae.Message);
                }

                input = this.inputHandler.ReadLine();
            }

            this.inputHandler.WriteLine(
                string.Join("\n", this.militaryRepository.Privates));
        }

        private void CreatePrivate(string type, string[] constrainData)
        {
            IPrivate @private =
                new FactoryMethod<IPrivate>()
                    .CreateFactory(
                        type, null, constrainData)
                    .Create();
            this.militaryRepository.AddPrivate(@private);
        }

        private void CreateEngineer(string type, string[] constrainData)
        {
            IEngineer engineer =
                new FactoryMethod<IEngineer>()
                    .CreateFactory(
                        type, null, constrainData)
                    .Create();
            if (engineer.CanExistSpecialisedSoldier())
            {
                this.militaryRepository.AddPrivate(engineer);
            }
        }

        private void CreateCommando(string type, string[] constrainData)
        {
            ICommando commando =
                new FactoryMethod<ICommando>()
                    .CreateFactory(
                        type, null, constrainData)
                    .Create();
            if (commando.CanExistSpecialisedSoldier())
            {
                this.militaryRepository.AddPrivate(commando);
            }
        }

        private void CreateLeutenantGeneral(string type, string[] constrainData)
        {
            ILeutenantGeneral leutenantGeneral =
                new FactoryMethod<ILeutenantGeneral>()
                    .CreateFactory(
                        type,
                        this.militaryRepository.Privates.ToArray(),
                        constrainData)
                    .Create();
            this.militaryRepository.AddPrivate(leutenantGeneral);
        }

        private void CreateSpy(string type, string[] constrainData)
        {
            ISpy spy =
                new FactoryMethod<ISpy>()
                    .CreateFactory(
                        type, null, constrainData)
                    .Create();
            this.militaryRepository.AddPrivate(spy);
        }
    }
}