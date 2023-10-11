
string path = "../../../../data/";
Filer filer = new Filer(path);
Demographic demographic = new Demographic(7, 9.81m, 10.82m, 0.03m ,50,10);
Policies policies = new Policies();
Disease disease = new Disease(0,0, 0, 0.014m);
Model model = new Model(filer, demographic, disease, policies);
model.run(100, false);
