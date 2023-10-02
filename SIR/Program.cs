
string path = "../../../../data/";
Filer filer = new Filer(path);
Demographic demographic = new Demographic(7,0,0,0.1m,50,10);
Disease disease = new Disease(0.95m,0, 0, 0);
Model model = new Model(filer, demographic, disease);
model.run(100, true);
