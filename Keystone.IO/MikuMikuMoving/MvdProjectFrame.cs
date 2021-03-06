﻿using System.IO;
using System.Linq;
using Linearstar.Keystone.IO;

namespace Linearstar.Keystone.IO.MikuMikuMoving
{
	public class MvdProjectFrame
	{
		public long FrameTime
		{
			get;
			set;
		}

		public float Gravity
		{
			get;
			set;
		}

		public float[] GravityVector
		{
			get;
			set;
		}

		public MvdSelfShadowModel SelfShadowModel
		{
			get;
			set;
		}

		public float SelfShadowDistance
		{
			get;
			set;
		}

		public float SelfShadowDeep
		{
			get;
			set;
		}

		public MvdProjectFrame()
		{
			this.Gravity = 0.98f;
			this.GravityVector = new[] { 0f, -1, 0 };
			this.SelfShadowDistance =
				this.SelfShadowDeep = 1;
		}

		public static MvdProjectFrame Parse(MvdProjectData pd, BinaryReader br)
		{
			var rt = new MvdProjectFrame
			{
				FrameTime = br.ReadInt64(),
			};

			if (pd.MinorType >= 1)
			{
				rt.Gravity = br.ReadSingle();
				rt.GravityVector = new[] { br.ReadSingle(), br.ReadSingle(), br.ReadSingle(), };
			}

			rt.SelfShadowModel = (MvdSelfShadowModel)br.ReadInt32();
			rt.SelfShadowDistance = br.ReadSingle();

			if (pd.MinorType >= 1 || br.GetRemainingLength() >= 4)
				rt.SelfShadowDeep = br.ReadSingle();

			return rt;
		}

		public void Write(MvdProjectData pd, BinaryWriter bw)
		{
			bw.Write(this.FrameTime);

			if (pd.MinorType >= 1)
			{
				bw.Write(this.Gravity);
				this.GravityVector.ForEach(bw.Write);
			}

			bw.Write((int)this.SelfShadowModel);
			bw.Write(this.SelfShadowDistance);
			bw.Write(this.SelfShadowDeep);
		}
	}
}
