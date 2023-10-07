
string path = "../../../../data/";
Filer filer = new Filer(path);
Demographic demographic = new Demographic(7, 9.81m, 10.82m, 0.1m,50,10);
Disease disease = new Disease(0.95m,0, 0, 0.014m);
Model model = new Model(filer, demographic, disease);
model.run(1000, true);
