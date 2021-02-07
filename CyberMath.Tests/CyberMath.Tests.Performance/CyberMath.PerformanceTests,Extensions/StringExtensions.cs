#region Using derectives

using System.Linq;
using System.Text;
using BenchmarkDotNet.Attributes;

#endregion

namespace CyberMath.PerformanceTests_Extensions
{
	public class StringExtensions
	{
		public string s =
			"NuNc vel vuLPUtAtE LORem, ET cOndimEntum niSI. pelLEnteSque AC DiGNIssiM libeRo. pHAsElLUs TUrPiS sApiEn, aLIquAm at BibENdum AC, laoreet sEd lECtUS. SUSpendISsE VEhICULa POSueRE sApiEN, Id dApIBus Mi FeUGIAt id. QuiSqUE id aUGUE NoN MASsA ULTricies condimENtUm. qUiSQUe In MAsSa Sit Amet niSl SolLIcitudiN RuTrUm EU QuIs DUI. DuIS A FErmEntum mI. aENEaN vitAe LECtuS tINCiDUNT, acCumSan FELiS Id, saGittIS torTOR. Vivamus PosUERE VEsTibUlum effIcITUR. cRAs FermEntuM efFiCItUR pURus TRiSTIQuE soDAleS. clASs aPtenT tacITi sociOSqu ad liTORA tOrquenT pER coNuBIa nOstRa, pER inCeptos HIMeNaeos. sEd cONGue masSa nunc, a VEstibuLuM Neque sAgitTis vehicUlA. uT sCeLerISque ACcuMSAn TristIqUE.SuSpeNDiSSE sIt AmET bLAndiT eRoS. PellENteSQUE FINibuS ErOS VEL PulVInaR vesTibUlUm. seD vITae ELEMenTuM LIBerO. CurabitUR ViveRRa eX sIT aMEt oRcI PORTTitOR, VULPuTATe vULpUTATe mAgna PretIUm. QUiSQUE FAuCiBuS, EST id PretIUM sEmper, uRna LibeRo tRIStiQUE teLlUs, IN GRAvIDa juSTo LiGulA IN ErOS. SED UltriCEs fERMENTum LobOrTIs. AlIQUam Ut ElEMeNtUM Elit, In TEMpOr loREm. PelLENteSQuE CuRsus nIBh EnIm, viTAe sODaLES TOrToR fACiLIsis aT. sUsPEnDisSe EU MagNA moLlIs, FiNibUS nisI Id, IacUlIs teLlUS.pElLeNTESqUe HabitAnt morBI TRisTiQUE SENeCtUs Et nETuS eT maLESuADa FAMeS AC TUrpiS eGesTaS. cUraBituR SiT amet QUam At LIGulA DiCTum finIbUS qUis Ac NisI. nullaM iD imperDiEt LACus. MaecENAS eu viVERRa nibh, AC ruTRum jusTO. phASEllUS vEStIBuLUm cONVaLlis ex, qUIs EGesTas mASSa sCelErISQUe SiT aMeT. proIn orNaRe sUscIpit INtErdUM. CuRABiTUr seMpER aRCU IN CoMMoDo pHAreTrA. aEnEAn UT tUrPIS aT eX molESTIe fermEnTUm. fUSCE MoLliS ORNAre anTE IN sEMper. ETIaM aT FeRmENTUm eNiM. nUnC Ex laCUs, rUtRUM vITae elit ut, SCELeRISQuE conGuE aUguE. eTiAm sceLEriSQuE BlANdIt SUSciPiT. vEStiBuluM AUctor TRISTIQue tORtoR, iN FAUCIbuS LIbERo SeMPer UT. iN Hac hAbitasse PLaTea DIcTumsT.PrAEsEnT quiS nUNC ET NisL COMMOdO pOrtA. In HAc HABItASSe plateA DIctUmST. FusCE nIbh qUAM, ViVeRra aT ipSum NON, DaPIbus pOsuEre TeLlUS. pROin VARius VeHICULa TORTOR, IN LaCINiA SApIEN COnseQUAt ULtRiciEs. MoRBI Mi ArCU, OrNARE siT AMET nullA siT aMET, sOdaLES PorttitoR fEliS. pHasElLUs OrnaRE luCTUS iPsUm Ac EGEsTAS. iNTeGer curSUs SEm RISus, FaUciBUs veNENatiS RISUs PreTIUm sIt AMET. ALiQuAM TempUS MeTus est, QUIS ALIQUAM mAURiS AliQUAm ut. etiaM a temPOr LECTuS, A faCiLIsIS LECtUS. eTIAm ut MOllis LECTus, iD VIvERRa aNTe. CURABItur PlACErAt Ante NeC GrAVIDA plaCeRaT. CRas LAciNIA cOnseCteTUR IaculIs. CLAss ApTeNT taCITI SocIosqU aD litOra toRQueNt PER conubiA nOsTrA, peR inceptOs HimeNaEOS. CRAS UlTriCIeS maGNa LEctus, EU posUerE Sem ELEMEnTum IN.pROin sed BlaNDIt DOlOR. SEd VeL aLIQueT lACUs, SED sodaLeS dUi. NaM orNare uRNA Vel UrNa PoRTTIToR, ut luCTus orcI tEmpOR. AEneAn sIt AMet maLeSUAdA JusTO. Nam RHOncUs DIgnIsSIM pUlvinAR. moRbi uLtRicies, quAm NOn GRaVidA euISMod, urnA LEO VeHiCuLA RIsuS, NEC HEnDRERIT RISUs tORTOR EgEt arCu. MOrbi moLESTIe CoNSEcTeTUr EX AT voLUtPat.PrAeSenT NulLa NiSL, mAttIs uT FeliS Id, CoNDImeNTUm sODaLEs eNIm. vEStibULUm DoloR eroS, LoBoRtiS vITae vaRiUS EGET, FiNIBus EGet MaSsA. NuLla Luctus VehicULa gRaVidA. SeD LacINia, nIbh A dICtum soDAlEs, JUsto JuSto tiNCidUnt magNA, Nec ElEmentum DIAm maSSA Sed mEtuS. SeD elEIfEnD LeO in lorEM vestIbuluM, uT TInCIDUnt doLOr pHareTRa. VivaMus ArcU Ex, ACCUMsAn cONSeCtEtUr TORtor quIS, oRNARE pellENteSQUE eRat. vEStIBUlum coNSeCtetUr faCILISIs cOnvAlliS. MAuRIs aT meTUS SolliCitudiN, tincidUnt aUgUE A, SeMpeR TellUs. qUisquE COngUE, MetuS noN dAPIbUs CONdImenTUM, anTE metUs tEMPOR NuNc, Et MAxIMUs auGUE diaM aT ANTE.QUIsQue sODALEs dolOr noN laCuS RuTRUm COmmOdo. mAEcenAs MOLlis ACcUMsAn NISI, QUiS saGITTIs massa SoLlicItUdin Eget. phAsELLuS MauRiS mAssA, scELeRisQue qUis auCtor id, fEUgiat IN IpSUm. AENEAn erAT AntE, PoRttiTOr aC RUtRuM sIT AmET, FerMENtum ID NequE. CuRAbiTUR vEl coMModO FEliS, ViTae MoLEstIE mETus. maURiS nEC pORtTItoR MAssA, aC dICtuM LECTUS. aliquAM COnSeCTetuR frIngILLA eRAt, aC RhoNCuS Lorem pUlVINaR digNISSIM. SeD MAttis fACilisiS lOREM. CuRabITUr VItae IPSuM ET eX FaUCIbUs IAcUlis SED SIt AmeT Mi.donEc VEL LeCTUs VeNeNatis, sAgITtiS quam NeC, VesTiBuLum auGUE. alIqUAm ErAt voLUTpaT. uT grAviDA DiAM sed UlLAmCorper sUSciPit. nUnC pulviNar In nisl neC LOBorTis. pHaseLlUS luCTuS pOSUERE IpSuM NEC cOnsecTeTur. MaURis Non ViverrA RIsUS, Eget MaXImus MAGna. SeD NISi LACuS, tempUS ET Metus SIt aMET, efFICitUR CommOdo iPSUm. donEc iD magnA ID sapIeN frIngiLla saGIttis. nULLam bLandiT nisL iN mI tEMPoR FerMeNtum. seD tEMpOR grAviDA leO, SED CONVaLlIs arCU orNArE VeL. MoRbi FAUCibUS FEUgiaT ANTe, QuIS PORtTitOr nUNc COmmodO NeC. MAUrIS veStIbULUm NEqUe SEd LIBERO DApIbus PortTITor. FuscE vENeNaTis eLIT NeC AnTe sOllIciTUdin, uT efFicITUr turpis henDrerIT. vEsTiBuLuM cOndiMenTum niSl QuIS nUnc FEuGIaT sUsciPit. Duis aLIQuet LIBeRo Eu VeLiT DapIbUs, NEC UlTRIces tELluS ViVerra. CURABitUr vItAE imPeRDIeT ESt.naM conDImenTUM esT id fRinGiLLa maXImUS. inTEGer QUIS TorTor in JUsTO orNARE FerMENtum. nam EgeSTAS LEcTuS uT IPSUM iMPERdIet, Ac aCCumsAn TurpIS fRingIllA. Donec cONSEQUAT maximus mAxiMuS. QUiSqUE MaLEsuada AT aRCU eU dapibus. PeLlENTesqUe ConsEquat sem SIt AMET OdIo conVAllis TIncIDUnT. MAeceNAS Et OrCI porTtITOR, PoSUeRe MAgNA cONvaLlIs, PORtTITor fElIS. MaEcENas eRoS ex, FERMeNtUM eT iACuliS at, diGNISsIm aT niBh. qUIsqUe SEd diGNissIm mEtUS. PraEsENt maTtIS nisl eGET libERo dapiBus temPoR. etiAm seD seM tinCidunT, poSuere FEliS ViTae, aLiqUet NeQUe.cRas EGesTaS mAlEsuaDA AUgUE, Id oRnarE lIgula fiNibus aC. maurIS Sit amet libERo nIsI. cRAs EGET vehiCUlA uRna, VEl Posuere NiSl. MAUris nIbH orci, PuLvINAr iN pOsuEre Vitae, SUSCiPiT et iPSuM. sed NEc MauRIs PuRUs. sEd id ELeifENd SEm. NUNC iMpERdieT PhAreTRA VElIT NEc acCuMSan. donec VoLuTPat pOSueRe MaGNa. CLASS aPTeNT tACiti sOcIoSQu aD lITORa TOrquenT pEr conUbIA nOsTra, pER iNcEptOS HIMeNAEOS. vEstiBULuM ANte IpsuM pRIMIS in faucibuS ORCi LuCTus et UltRices POsuere CUbilIa curae; nuNc HEndREriT AuguE leCtUS, mAxiMUs puLVInAR lACUs cUrsuS vEl.\r\nNUlLa rUTRuM ORcI UT eST sodAlEs PLaCErat. VIVAmUS UlLAmCoRPER, tElLUs at RuTRum diCTum, PuRUs AUGuE fAcIlISis JUsTO, viTAe CURSuS tuRPis puRuS IN eLit. phAsELLus IN mATtIs NuLlA, AC FiniBuS ESt. sUspEndisSe eraT nIbh, peLLeNTesQUe NeC dIcTUM EU, DICtUm et RISUS. UT AT ScELerIsquE maURIs. fUSce sED VOLutpat LOreM, EGEt acCumSan ERAT. IN nUnc duI, MOllIs Id tINcIdUNT Ac, DictUM iN ORcI. sED cOngUE tINcidUNt URnA, VItAe MaleSuADA NuLLa alIqUAM Et. DUis seD aCcuMSaN SEm. mORBi tiNCiDUNT soLLicITUDIn Nisl sEd Congue. MaECEnAs ut libero ut SeM tIncIDUnt cursUs vEl uT Ex. crAS At puRUs Sed eniM TeMpOr pReTIuM.NUlLa GRaviDa lACUs FriNGILLA eROs EFFicITUr, VEl EUisMOD EX PULVinAR. prOin eU LEctUS LECTUS. CRas riSUS lAcus, graViDa EgeT eRaT id, PUlvInarIMperDieT.";

		[Benchmark]
		public void ToAlternatingCaseSB()
		{
			var answer = new StringBuilder("", s.Length);

			foreach (var c in s)
			{
				if (char.IsLower(c))
					answer.Append(char.ToUpper(c));
				else if (char.IsUpper(c))
					answer.Append(char.ToLower(c));
				else
					answer.Append(c);
			}
		}

		[Benchmark]
		public void ToAlternatingCaseOverKill()
		{
			var length = s.Length;
			var output = new char[length];

			int i = 0, j = length - 1;
			while (i <= j)
			{
				output[i] = ShiftCase(s[i++]);
				output[j] = ShiftCase(s[j--]);
			}
		}

		private char ShiftCase(char c)
		{
			var i = (int)c;
			if (i < 65 ||
			    i > 122)
				return c;

			if (i < 97 &&
			    i > 90)
				return c;

			return (char)(i > 90 ? c & ~0x20 : c | 0x20);
		}

		[Benchmark]
		public void ToAlternatingCaseOverLinq() =>
			string.Concat(s.Select(c => (char)(c ^ (c / 65 * (1 - (c - 1) % 32 / 26) * 32))));

		[Benchmark]
		public void ToAlternatingCaseLinq() =>
			string.Concat(s.Select(c => char.IsUpper(c) ? char.ToLower(c) : char.ToUpper(c)));
	}
}